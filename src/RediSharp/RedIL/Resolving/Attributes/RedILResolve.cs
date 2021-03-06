using System;

namespace RediSharp.RedIL.Resolving.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Constructor | AttributeTargets.Field)]
    class RedILResolve : Attribute
    {
        private byte _resolverEnum;

        private Type _resolverType;

        public object[] Arguments { get; set; }

        public RedILResolve(Type resolverType, params object[] arguments)
        {
            if (resolverType.IsSubclassOf(typeof(RedILMethodResolver)))
            {
                _resolverEnum = 0;
            }
            else if (resolverType.IsSubclassOf(typeof(RedILMemberResolver)))
            {
                _resolverEnum = 1;
            }
            else if (resolverType.IsSubclassOf(typeof(RedILObjectResolver)))
            {
                _resolverEnum = 2;
            }
            else if (resolverType.IsSubclassOf(typeof(RedILValueResolver)))
            {
                _resolverEnum = 3;
            }
            else
            {
                throw new RedILException($"Type '{resolverType}' is not a resolver");
            }

            _resolverType = resolverType;
            Arguments = arguments;
        }

        public RedILMethodResolver CreateMethodResolver()
        {
            if (_resolverEnum != 0)
            {
                throw new RedILException($"Unable to resolve method resolver");
            }
            
            return Activator.CreateInstance(_resolverType, Arguments) as RedILMethodResolver;
        }

        public RedILMemberResolver CreateMemberResolver()
        {
            if (_resolverEnum != 1)
            {
                throw new RedILException($"Unable to resolve member resolver");
            }
            
            return Activator.CreateInstance(_resolverType, Arguments) as RedILMemberResolver;
        }

        public RedILObjectResolver CreateObjectResolver()
        {
            if (_resolverEnum != 2)
            {
                throw new RedILException($"Unable to resolve object resolver");
            }
            
            return Activator.CreateInstance(_resolverType, Arguments) as RedILObjectResolver;
        }

        public RedILValueResolver CreateValueResolver()
        {
            if (_resolverEnum != 3)
            {
                throw new RedILException($"Unable to resolve value resolver");
            }
            
            return Activator.CreateInstance(_resolverType, Arguments) as RedILValueResolver;
        }
    }
}