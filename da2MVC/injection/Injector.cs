using MVC_Framework.injection;
using MVC_Framework.view;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using da2mvc.command;
using da2mvc.events;

namespace da2mvc.injection
{
    public class Injector
    {
        private static PrivateInjector injector = new PrivateInjector();

        public static void MapType(Type mapType, Type mapTo, bool isSingleton = false) { injector.MapType(mapType, mapTo, isSingleton); }
        public static void MapType(Type type, bool isSingleton = false) { injector.MapType(type, isSingleton); }
        public static void MapCommand(Type dispatcherType, string eventName, Type commandType) { injector.MapCommand(dispatcherType, eventName, commandType); }
        public static object GetInstance(Type type) { return injector.GetInstance(type); }
        public static void ExecuteCommand(Type commandType, BaseEventArgs eventArgs = null) { injector.ExecuteCommand(commandType, eventArgs); }
        public static void MapInstance(Type mapType, object instance) { injector.MapInstance(mapType, instance); }
        public static void MapInstance(object instance) { injector.MapInstance(instance); }
        public static void MapView(Type viewType, Type mediatorType, bool isSingleton = false) { injector.MapView(viewType, mediatorType, isSingleton); }
        public static void MapViewInstance(Type mapType, object view, Type mediatorType) { injector.MapViewInstance(mapType, view, mediatorType); }
        public static void MapViewInstance(object view, Type mediatorType) { injector.MapViewInstance(view, mediatorType); }

        private class PrivateInjector
        {
            private Dictionary<Type, TypeMapping> typeMappings = new Dictionary<Type, TypeMapping>();
            private Dictionary<Type, CommandMapping> commandMappings = new Dictionary<Type, CommandMapping>();
            private Dictionary<Type, Type> mediatorMappings = new Dictionary<Type, Type>();
            private Dictionary<IComponent, IMediator> mediators = new Dictionary<IComponent, IMediator>();
            private Dictionary<Type, object> singletons = new Dictionary<Type, object>();



            public void MapType(Type mapType, Type mapTo, bool isSingleton = false)
            {
                typeMappings.Add(mapType, new TypeMapping(mapType, mapTo, isSingleton));
            }

            public void MapType(Type type, bool isSingleton = false)
            {
                MapType(type, type, isSingleton);
            }

            public void MapView (Type viewType, Type mediatorType, bool isSingleton = false)
            {
                if (!typeof(IComponent).IsAssignableFrom(viewType))
                    throw new Exception($"Type {viewType} must implement IComponent.");

                if (!typeof(IMediator).IsAssignableFrom(mediatorType))
                    throw new Exception($"Type {mediatorType} must implement IMediator.");

                typeMappings.Add(viewType, new TypeMapping(viewType, viewType, isSingleton));
                mediatorMappings.Add(viewType, mediatorType);
            }

            public void MapViewInstance(object view, Type mediatorType)
            {
                MapViewInstance(view.GetType(), view, mediatorType);
            }

            public void MapViewInstance(Type mapType, object view, Type mediatorType)
            {
                Type viewType = view.GetType();

                if (!(view is IComponent))
                    throw new Exception($"Type {viewType} must implement IComponent.");

                if (!typeof(IMediator).IsAssignableFrom(mediatorType))
                    throw new Exception($"Type {mediatorType} must implement IMediator.");

                typeMappings.Add(mapType, new TypeMapping(mapType, viewType, true));
                singletons.Add(mapType, view);
                mediatorMappings.Add(mapType, mediatorType);
                MapMediator(mapType, view);
                MapEvents(mapType, view);
            }

            public void MapInstance(object instance)
            {
                MapInstance(instance.GetType(), instance);
            }

            public void MapInstance(Type mapType, object instance)
            {
                typeMappings.Add(mapType, new TypeMapping(mapType, instance.GetType(), true));
                singletons.Add(mapType, instance);
                MapEvents(mapType, instance);
            }

            public void MapCommand(Type dispatcherType, string eventName, Type commandType)
            {
                if (!typeof(IEventDispatcher).IsAssignableFrom(dispatcherType))
                    throw new Exception($"Type {dispatcherType} must implement IDispatcher.");

                if (!typeof(ICommand).IsAssignableFrom(commandType))
                    throw new Exception($"Type {commandType} must implement ICommand.");

                if (!commandMappings.ContainsKey(dispatcherType))
                {
                    CommandMapping mapping = new CommandMapping(dispatcherType);
                    mapping.MapEvent(eventName, commandType);
                    commandMappings.Add(dispatcherType, mapping);
                }
                else
                {
                    CommandMapping mapping = commandMappings[dispatcherType];
                    mapping.MapEvent(eventName, commandType);
                }
            }

            public object GetInstance(Type type)
            {
                if (!typeMappings.ContainsKey(type))
                    throw new Exception($"No mapping for type {type}.");

                TypeMapping mapping = typeMappings[type];

                if (mapping.IsSingleton)
                {
                    if (singletons.ContainsKey(type)) return singletons[type];

                    object instance = BuildInstance(mapping.MapTo, GetCtorParameters(mapping.MapTo));
                    singletons.Add(type, instance);
                    return instance;
                }

                return BuildInstance(mapping.MapTo, GetCtorParameters(mapping.MapTo));
            }

            public void ExecuteCommand(Type commandType, BaseEventArgs eventArgs=null)
            {
                GetCommandInstance(commandType, eventArgs).Execute();
            }

            private void ExecuteCommandFromEvent(object sender, EventArgs args)
            {
                BaseEventArgs typedArgs = (BaseEventArgs)args;

                CommandMapping mapping = commandMappings[sender.GetType()];

                if (!mapping.HasMapping(typedArgs.EventName))
                    return;

                foreach (Type commandType in mapping.GetCommandTypes(typedArgs.EventName))
                {
                    ICommand command = GetCommandInstance(commandType, typedArgs);
                    command.Execute();
                }
            }

            private void MainEventHandler(object sender, EventArgs args)
            {
                BaseEventArgs typedArgs = (BaseEventArgs)args;

                // Registered commands
                if (commandMappings.ContainsKey(sender.GetType()))
                {
                    ExecuteCommandFromEvent(sender, args);
                }
                // Registered mediators
                else if (mediators.Count > 0)
                {
                    // Do a copy cause: some mediators could trigger instanciation of a new view/mediator and then modify the mediators list while we're iterating on it.
                    List<IMediator> mediatorList = mediators.Values.ToList();

                    foreach (IMediator mediator in mediatorList)
                    {
                        if (mediator.Listeners.ContainsKey(sender.GetType()))
                        {
                            MediatorEventMapping mapping = mediator.Listeners[sender.GetType()];
                            if (mapping.HasListener(typedArgs.EventName))
                                mapping.GetListener(typedArgs.EventName)(typedArgs);
                        }
                    }
                }
            }

            private ICommand GetCommandInstance(Type type, BaseEventArgs eventArgs)
            {
                return (ICommand)BuildInstance(type, GetCtorParameters(type, eventArgs));
            }

            private object[] GetCtorParameters(Type type, EventArgs eventArgs = null)
            {
                ConstructorInfo ctorInfo = type.GetConstructors()[0];
                ParameterInfo[] parameters = ctorInfo.GetParameters();

                object[] ctorParams = new object[parameters.Length];
                int paramCount = 0;
                foreach (ParameterInfo param in parameters)
                {
                    if (eventArgs != null && param.ParameterType.IsAssignableFrom(eventArgs.GetType()))
                    {
                        ctorParams[paramCount] = eventArgs;
                    }
                    else
                    {
                        object instanceParam = GetInstance(param.ParameterType); // TODO boucle infinie si on met le Type1 en param de Type1 ?
                        ctorParams[paramCount] = instanceParam;
                    }
                    paramCount++;
                }

                return ctorParams;
            }

            private object BuildInstance(Type type, object[] parameters)
            {
                object instance = Activator.CreateInstance(type, parameters);

                MapMediator(type, instance);
                MapEvents(type, instance);

                return instance;
            }

            private void MapMediator(Type type, object view)
            {
                if (view is IComponent && mediatorMappings.ContainsKey(type))
                {
                    IComponent typedView = (IComponent)view;
                    Type mediatorType = mediatorMappings[type];
                    IMediator mediator = (IMediator)BuildInstance(mediatorType, GetCtorParameters(mediatorType));
                    mediator.InitializeView(typedView);
                    mediators.Add(typedView, mediator);
                    typedView.Disposed += ViewWithMediatorDisposed;
                }
            }

            private void ViewWithMediatorDisposed(object sender, EventArgs e)
            {
                mediators.Remove((IComponent)sender);
            }

            private void MapEvents(Type type, object instance)
            {
                if (instance is IEventDispatcher)
                {
                    IEventDispatcher dispatcher = (IEventDispatcher)instance;
                    dispatcher.MvcEventHandler += MainEventHandler;
                }

                /*
                // Old way to do it.
                if (commandMappings.ContainsKey(type))
                {
                    CommandMapping mapping = commandMappings[type];
                    IDispatcher dispatcher = (IDispatcher)instance;
                    dispatcher.MyEventHandler += ExecuteCommandFromEvent;
                }*/
            }
        }
    }

    
}