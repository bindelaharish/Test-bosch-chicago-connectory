#pragma warning disable 1591
using System;
using System.Linq;
using Omu.ValueInjecter.Injections;

namespace RB.JobAssistant.Models.Mapper
{
    public class IgnoreProps : LoopInjection
    {
        private readonly string[] _ignores;

        public IgnoreProps()
        {
            _ignores = null;
        }

        public IgnoreProps(params string[] ignores)
        {
            _ignores = ignores;
        }

        protected override bool MatchTypes(Type source, Type target)
        {
            return (_ignores == null || !_ignores.Contains(source.Name)) &&
                   source.Name == target.Name
                   && source.DeclaringType == target.DeclaringType;
        }
    }
}