using System;
using Zeebe.Client.Api.Commands;
using Zeebe.Client.Bootstrap.Abstractions;

namespace Zeebe.Client.Bootstrap.Extensions
{
    public static class StateBuilderExtensions
    {
        private static IZeebeVariablesSerializer _serializer;

        private static IZeebeVariablesSerializer GetSerializer()
        {
            if(_serializer == null)
                throw new NullReferenceException($"{nameof(IZeebeVariablesSerializer)} has not been set with an instance. Please use the {nameof(StateBuilderExtensions)}.{nameof(Configure)} method to setup an instance.");

            return _serializer;
        }

        public static void Configure(IZeebeVariablesSerializer serializer) => _serializer = serializer;

        public static IPublishMessageCommandStep3 State<TState>(this IPublishMessageCommandStep3 step, TState state)
            where TState : class, new()
        {
            if(state == null)
                throw new ArgumentNullException(nameof(state));

            var variables = GetSerializer().Serialize(state);
            return step.Variables(variables);
        }

        public static ICreateProcessInstanceCommandStep3 State<TState>(this ICreateProcessInstanceCommandStep3 step, TState state)
            where TState : class, new()
        {
            if(state == null)
                throw new ArgumentNullException(nameof(state));

            var variables = GetSerializer().Serialize(state);
            return step.Variables(variables);
        }

        public static ISetVariablesCommandStep2 State<TState>(this ISetVariablesCommandStep1 step, TState state)
            where TState : class, new()
        {
            if(state == null)
                throw new ArgumentNullException(nameof(state));

            var variables = GetSerializer().Serialize(state);
            return step.Variables(variables);
        }
    }
}