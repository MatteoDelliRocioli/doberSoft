using System;
using System.Collections.Generic;
using System.Text;

namespace doberSoft.Util
{
    public static class iUID
    {
        private readonly static int maxSpan = 2147483647
;
        private static int _uid =1;
        /// <summary>
        /// Returns an integer starting from 1 up to 2147483647
        /// each call increments by 1.
        /// When the top value is reached it restarts from 1
        /// </summary>
        /// <returns></returns>
        public static int NewId ()
        {
            if (_uid > maxSpan)
                _uid = 1; 

            return _uid++;
        }  
    }
}
