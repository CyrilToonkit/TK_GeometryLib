using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TK.GeometryLib
{
    public static class DoubleHelper
    {
        // *** DOUBLE ***

        /// <summary>
        /// Parse a double correctly whatever the decimal separator
        /// </summary>
        /// <param name="inStringDouble">The double value as string</param>
        /// <returns>The parsed double, or 0 if we cannot parse</returns>
        public static double DoubleParse(string inStringDouble)
        {
            return DoubleParse(inStringDouble, 0);
        }

        /// <summary>
        /// Parse a double correctly whatever the decimal separator
        /// </summary>
        /// <param name="inStringDouble">The double value as string</param>
        /// <param name="inDefaultValue">The default value if we cannot parse</param>
        /// <returns>The parsed double, or 'inDefaultValue' if we cannot parse</returns>
        public static double DoubleParse(string inStringDouble, double inDefaultValue)
        {
            double rslt = inDefaultValue;

            if (double.TryParse(inStringDouble, out rslt))
            {
                return rslt;
            }
            else
            {
                inStringDouble = inStringDouble.Replace(",", ".");

                if (double.TryParse(inStringDouble, out rslt))
                {
                    return rslt;
                }
                else
                {
                    inStringDouble = inStringDouble.Replace(".", ",");

                    if (double.TryParse(inStringDouble, out rslt))
                    {
                        return rslt;
                    }
                }
            }

            return rslt;
        }

        /// <summary>
        /// Tells if two doubles are Equal within a tolerance
        /// </summary>
        /// <param name="inValue">First double</param>
        /// <param name="inRefValue">Second double</param>
        /// <param name="inTolerance">Tolerance</param>
        /// <returns>true if doubles are Equal within the tolerance</returns>
        public static bool DoubleIsFuzzyEqual(double inValue, double inRefValue, double inTolerance)
        {
            if (inTolerance == 0)
            {
                return inValue == inRefValue;
            }

            return (Math.Abs(inRefValue - inValue) < inTolerance);
        }

        // *** FLOAT ***

        /// <summary>
        /// Parse a double correctly whatever the decimal separator
        /// </summary>
        /// <param name="inStringDouble">The double value as string</param>
        /// <returns>The parsed double, or 0 if we cannot parse</returns>
        public static float FloatParse(string inStringFloat)
        {
            return FloatParse(inStringFloat, 0);
        }

        /// <summary>
        /// Parse a double correctly whatever the decimal separator
        /// </summary>
        /// <param name="inStringDouble">The double value as string</param>
        /// <param name="inDefaultValue">The default value if we cannot parse</param>
        /// <returns>The parsed double, or 'inDefaultValue' if we cannot parse</returns>
        public static float FloatParse(string inStringFloat, float inDefaultValue)
        {
            float rslt = inDefaultValue;

            if (float.TryParse(inStringFloat, out rslt))
            {
                return rslt;
            }
            else
            {
                inStringFloat = inStringFloat.Replace(",", ".");

                if (float.TryParse(inStringFloat, out rslt))
                {
                    return rslt;
                }
                else
                {
                    inStringFloat = inStringFloat.Replace(".", ",");

                    if (float.TryParse(inStringFloat, out rslt))
                    {
                        return rslt;
                    }
                }
            }

            return rslt;
        }

        /// <summary>
        /// Tells if two doubles are Equal within a tolerance
        /// </summary>
        /// <param name="inValue">First double</param>
        /// <param name="inRefValue">Second double</param>
        /// <param name="inTolerance">Tolerance</param>
        /// <returns>true if doubles are Equal within the tolerance</returns>
        public static bool FloatIsFuzzyEqual(float inValue, float inRefValue, float inTolerance)
        {
            if (inTolerance == 0)
            {
                return inValue == inRefValue;
            }

            return (Math.Abs(inRefValue - inValue) < inTolerance);
        }
    }
}
