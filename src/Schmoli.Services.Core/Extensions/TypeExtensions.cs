using System;

namespace Schmoli.Services.Core.Extensions
{
    public static class TypeNameExtensions
    {
        /// <summary>
        /// Converts a generic like Foo[bar] to a friendly name instead of Foo`1
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetFriendlyName(this Type type)
        {
            string friendlyName = type.Name;
            if (type.IsGenericType)
            {
                int iBacktick = friendlyName.IndexOf('`');
                if (iBacktick > 0)
                {
                    friendlyName = friendlyName.Remove(iBacktick);
                }
                friendlyName += "<";
                Type[] typeParameters = type.GetGenericArguments();
                for (int i = 0; i < typeParameters.Length; ++i)
                {
                    string typeParamName = GetFriendlyName(typeParameters[i]);
                    friendlyName += i == 0 ? typeParamName : "," + typeParamName;
                }
                friendlyName += ">";
            }

            return friendlyName;
        }
    }
}
