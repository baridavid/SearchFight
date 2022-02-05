using Application.Common.Exceptions;
using System;
using System.Collections.Generic;

namespace Application.Common
{
    public static class Extensions
    {
        public static T GetMaxValue<T>(this IEnumerable<T> source, Func<T, long> func)
        {
            if (source == null)
                throw new ApiException(SearchFightResource.msg_exception_engine_validation);

            using (var en = source.GetEnumerator())
            {
                if (!en.MoveNext())
                    throw new ApiException(SearchFightResource.msg_exception_engine_validation);

                long max = func(en.Current);
                T maxValue = en.Current;

                while (en.MoveNext())
                {
                    var possible = func(en.Current);

                    if (max < possible)
                    {
                        max = possible;
                        maxValue = en.Current;
                    }
                }
                return maxValue;
            }
        }

        public static int GetMaxValueTwo<T>(List<T> list, Converter<T, int> projection)
        {
            if (list.Count == 0)
            {
                throw new ApiException(SearchFightResource.msg_exception_engine_validation);
            }
            int maxValue = int.MinValue;
            foreach (T item in list)
            {
                int value = projection(item);
                if (value > maxValue)
                {
                    maxValue = value;
                }
            }
            return maxValue;
        }

    }
}
