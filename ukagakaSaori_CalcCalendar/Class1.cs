using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Marshal
using System.Runtime.InteropServices;
//match
using System.Text.RegularExpressions;

namespace ukagakaSaori_CalcCalendar {
    public class Class1 {
        //static string PATH ;

        [DllExport] 
        public static unsafe bool load( IntPtr h , int len ) {
            Console.WriteLine( "Load SAORI Calc Calendar" );
            //PATH = Marshal.PtrToStringAnsi( h , len );

            Marshal.FreeHGlobal(h);
            return true;
        }
        [DllExport] 
        public static unsafe bool unload() {
            Console.WriteLine( "Unload SAORI Calc Calendar" );
            return true;
        }

        [DllExport] 
        //C#だとint型か。
        //しかもnull文字を含まない長さだ。
        public static unsafe IntPtr request( IntPtr h, IntPtr len){
            bool   CheckVersion      = false;
            string Argument0         = "";
            string Argument1         = "";
            string Argument2         = "";
            string Argument3         = "";
            string Argument4         = "";
            string Argument5         = "";

            //返り値。状況によって上書き。
            string resString = "SAORI/1.0 204 No Content\r\n\r\n";

            //文字数
            int req_len = Marshal.ReadInt32( len );
            //文字
            string req = Marshal.PtrToStringAnsi( h , req_len );
            Marshal.FreeHGlobal(h);


            Console.WriteLine( req );
            string[] sep = {"\r\n"};
            string[] requestText = req.Split( sep , StringSplitOptions.None );
            foreach ( string requestLine in requestText ) {
                //Console.WriteLine( "check : " + requestLine );
                
                if ( Regex.IsMatch( requestLine , "GET Version SAORI/1.0") ) {
                    Console.WriteLine( "Check Version" );
                    CheckVersion = true;
                }

                if ( Regex.IsMatch( requestLine , "Argument0: .+") ) {
                    string[] lineSep = {"Argument0: "};
                    string[] lineValue = requestLine.Split( lineSep , StringSplitOptions.None );
                    Argument0 = lineValue[1] ;
                }

                if ( Regex.IsMatch( requestLine , "Argument1: .+") ) {
                    string[] lineSep = {"Argument1: "};
                    string[] lineValue = requestLine.Split( lineSep , StringSplitOptions.None );
                    Argument1 = lineValue[1] ;
                }

                if ( Regex.IsMatch( requestLine , "Argument2: .+") ) {
                    string[] lineSep = {"Argument2: "};
                    string[] lineValue = requestLine.Split( lineSep , StringSplitOptions.None );
                    Argument2 = lineValue[1] ;
                }

                if ( Regex.IsMatch( requestLine , "Argument3: .+") ) {
                    string[] lineSep = {"Argument3: "};
                    string[] lineValue = requestLine.Split( lineSep , StringSplitOptions.None );
                    Argument3 = lineValue[1] ;
                }

                if ( Regex.IsMatch( requestLine , "Argument4: .+") ) {
                    string[] lineSep = {"Argument4: "};
                    string[] lineValue = requestLine.Split( lineSep , StringSplitOptions.None );
                    Argument4 = lineValue[1] ;
                }

                if ( Regex.IsMatch( requestLine , "Argument5: .+") ) {
                    string[] lineSep = {"Argument5: "};
                    string[] lineValue = requestLine.Split( lineSep , StringSplitOptions.None );
                    Argument5 = lineValue[1] ;
                }
            }

            if ( CheckVersion != true ){
                int IntYear         = Convert.ToInt32( Argument0 );
                int IntMonth        = Convert.ToInt32( Argument1 );
                int IntDay          = Convert.ToInt32( Argument2 );
                int IntAddYear      = Convert.ToInt32( Argument3 );
                int IntAddMonth     = Convert.ToInt32( Argument4 );
                int IntAddDay       = Convert.ToInt32( Argument5 );

                DateTime ResDate = new DateTime( IntYear , IntMonth , IntDay ).AddYears( IntAddYear ).AddMonths( IntAddMonth ).AddDays( IntAddDay );
                //Console.WriteLine( ResDate ); 

                string ResYear        = Convert.ToString( ResDate.Year ) ;
                string ResMonth       = Convert.ToString( ResDate.Month );
                string ResDay         = Convert.ToString( ResDate.Day );

                string Res0           = ResYear + "/" + ResMonth + "/" + ResDay ;

                resString = "SAORI/1.0 200 OK\r\nCharset: Shift_JIS\r\nResult: " + Res0 + "\r\nValue0: " + ResYear + "\r\nValue1: " + ResMonth + "\r\nValue2: " + ResDay + "\r\n\r\n";
            }
            //resString = "SAORI/1.0 200 OK\r\nCharset: Shift_JIS\r\nResult: 2000/03/01\r\nValue0: OOOO\r\nValue1: 1111\r\n\r\n";


            //realloc サイズの再割り当て
            //null文字は考慮する必要はなかったが、StringToHGlobalAnsiはnull文字を含む。
            int str_len = resString.Length;
            Marshal.WriteInt32( len , 3 * str_len + 1 );
            IntPtr res = Marshal.StringToHGlobalAnsi( resString );
            return res;
        }


    }
}
