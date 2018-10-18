#pragma warning disable 1591
using Omu.ValueInjecter.Injections;
using System;

namespace RB.JobAssistant.Models.Mapper
{
   public class BasicFilterId : LoopInjection
    {
        //sourcePropName "Guid" will not map to target property name "Guid"
        //ie: Keep target property value as it is (not change from mapping)
        protected override bool MatchTypes(Type source, Type target)
        {
            if (source != null)
            {
                return source.Name !=  "Guid";
            }
            else return false;
        }
    }
}