using System;
using System.Drawing;
using System.Drawing.Imaging;
using Nvidia.AtpLib;
using System.Windows.Automation;
using System.Threading;

namespace Assignment2
{
    class Assignment2_Helper
    {
        public static int countDigit(long n)
        {
            int count = 0;
            if(n==0)
            {
                ++count;
            }
            while (n != 0)
            {
                n = n / 10;
                ++count;
            }
            return count;
        }

        public static int ConvertPositivetoNegative(int input,out bool isNegative)
        {
            if (input < 0)
            {
                isNegative = true;
                return (input * (-1));
            }
            else
            {
                isNegative = false;
                return input;
            }

        }
        public static void IsPressedfunction(bool ispress,string elementName)
        {

            if (ispress)
                Console.WriteLine(elementName + "Button pressed");
            else
                Console.WriteLine(elementName + "Button not pressed");

        }
        public static void IsSucecssfunction(bool issucess)
        {
            if (issucess)
                Console.WriteLine("PROGRAM SUCCESSFULLY EXECUTED\n\n");
            else
                Console.WriteLine("\n PROGRAM NOT* SUCCESSFULLY EXECUTED\n\n");
        }


        public static bool All_calcultor_Operations(UIA uia, int i, int j, AutomationElement AE_Calc, bool is_Cal_already_opened)
        {
            try
            {
                bool isSuccess = false;
                bool isPressed_negative1 = false;
                bool isPressed_negative2 = false;
                bool is_negative1 = false, isPressed_clear = false;
                bool is_negative2 = false;
                int int_input1, int_input2;
                int int_input1_positive, int_input2_positive;
                string compare = string.Empty;
                string Equal_to = "Equal to";
                string str_operation_name;

                Console.WriteLine(@"Give inputs as a Integers** and Operation** among (*,/,-,+), which you wants to perform on calulcator automatically");
                Console.WriteLine("\n");
                Console.WriteLine(@"NOTE:Give inputs as a Integers** & should be between (-2,147,483,648 to 2,147,483,647)! ");              

                Console.WriteLine("\n INPUT 1: ");
                while (!int.TryParse(Console.ReadLine(), out int_input1))
                    Console.Write("The value must be of integer** type, try again:\n ");   
                int_input1_positive = ConvertPositivetoNegative(int_input1,out is_negative1);

                Console.WriteLine("\n INPUT 2: ");
                while (!int.TryParse(Console.ReadLine(), out int_input2))
                    Console.Write("The value must be of integer type, try again:\n ");
                int_input2_positive = ConvertPositivetoNegative(int_input2, out is_negative2);

                Console.WriteLine("\n\n Give operation: ");
                do
                {
                    string str_operation = Console.ReadLine();
                    if(String.Equals(str_operation, "+") || String.Equals(str_operation, "-") || String.Equals(str_operation, "*") || String.Equals(str_operation, "/"))
                    {
                       str_operation_name = Coversion_sign_to_String(str_operation);
                        break;
                    }
                    else
                    {
                        Console.Write("The value must be among (+,-,*,/) type, try again:\n ");
                    }
                } while (true);

                Console.WriteLine("\n\n");
                Console.WriteLine("What You've Entered:");
                Console.WriteLine(int_input1);
                Console.WriteLine(int_input2);                
                Console.WriteLine("\n\n\n Execution Starts !!!\n______________________________________\n______________________________________\n");

                if (is_Cal_already_opened)
                {
                    Nvidia.AtpLib.UIAHelper.SetWindowNormal("Calculator");
                    Nvidia.AtpLib.UIAHelper.BringItToForeground(AE_Calc.Current.NativeWindowHandle);
                }
                else
                {
                    Console.WriteLine("Calculator-already-not-open-case...Opening..!!\n");
                }

                if ((String.Equals(Convert.ToString(int_input2), compare)) || (String.Equals(Convert.ToString( int_input2), compare)) || (String.Equals(str_operation_name, compare)))
                {
                    Console.WriteLine(@"Either Input1 or Input2 or Sign Input is not valid....REENTER or RERUN !!\n");
                }
                else
                {

                    if (AE_Calc != null)
                    {
                        Console.WriteLine(i++ + "." + AE_Calc.Current.Name + " is not null and assigned to variable AE_Calc");
                        AutomationElement AE_group = uia.GetElementByClassName(AE_Calc, "LandmarkTarget", true, 300);
                        if (AE_group != null)
                        {
                            Console.WriteLine(i++ + "." + AE_group.Current.Name + " is not null and assigned to variable AE_group");
                            AutomationElement AE_Number_pad = uia.GetElementById(AutomationElement.RootElement, "NumberPad", true);
                            AutomationElement AE_Negate = uia.GetElementById(AutomationElement.RootElement, "negateButton", true);
                            AutomationElement AE_Clear = uia.GetElementById(AutomationElement.RootElement, "clearButton", true);
                            if (AE_Number_pad != null)
                            {
                                Console.WriteLine(i++ + "." + AE_Number_pad.Current.Name + " is not null and assigned to variable AE_Number_pad");
                                Thread.Sleep(2000);
                                isPressed_clear = uia.ClickElementMid(AE_Clear);
                                Thread.Sleep(1000);
                                IsPressedfunction(isPressed_clear, "Clear");

                                int digitsInInput1 = countDigit(int_input1);
                                while (digitsInInput1 > 0)
                                {
                                    int remender_digit1 = Convert.ToInt32(Math.Floor(int_input1_positive / (Math.Pow(10, digitsInInput1 - 1))));
                                    AutomationElement AE_input1 = uia.GetElementById(AE_Number_pad, ("num" + remender_digit1 + "Button"), true);

                                    bool isPressed_input1 = false;
                                    isPressed_input1 = uia.ClickElementMid(AE_input1);
                                    Thread.Sleep(2000);
                                    IsPressedfunction(isPressed_input1, Convert.ToString(remender_digit1));
                                    int_input1_positive = Convert.ToInt32(Math.Floor(int_input1_positive % (Math.Pow(10, digitsInInput1 - 1))));
                                    digitsInInput1--;
                                }
                                if (is_negative1)
                                {
                                    isPressed_negative1 = uia.ClickElementMid(AE_Negate);
                                    IsPressedfunction(isPressed_negative1," NEGATION ");
                                }
                                Console.WriteLine("\n\n\n CLICKING " + int_input1 + " !!");
                                Thread.Sleep(2000);
                                CaptureMyScreen(j++);

                                AutomationElement AE_Standard_operators = uia.GetElementById(AE_group, "StandardOperators", true);
                                if (AE_Standard_operators != null)
                                {
                                    AutomationElement AE_operation = uia.GetElementById(AE_Standard_operators, (str_operation_name + "Button"), true);
                                    AutomationElement AE_equal_to = uia.GetElementByControlTypeAndName(AE_Standard_operators, ControlType.Button, "Equals");

                                    Console.WriteLine(@"CLICKING " + str_operation_name + "Button" + " !!");
                                    Thread.Sleep(1000);
                                    bool isPressed_operation = false;
                                    isPressed_operation = uia.ClickElementMid(AE_operation);
                                    IsPressedfunction(isPressed_operation, str_operation_name);
                                    CaptureMyScreen(j++);


                                    Console.WriteLine("CLICKING " + int_input2 + " !!");
                                    Thread.Sleep(1000);
                                    bool isPressed_input2 = false;                          
                                    int digitsInInput2 = countDigit(int_input2_positive);

                                    while (digitsInInput2 > 0)
                                    {
                                        int remender_digit2 = Convert.ToInt32(Math.Floor(int_input2_positive / (Math.Pow(10, digitsInInput2 - 1 ))));
                                        //Console.WriteLine(remender_digit2);

                                        AutomationElement AE_input2 = uia.GetElementById(AE_Number_pad, ("num" + remender_digit2 + "Button"), true);
                                        isPressed_input2 = false;
                                        isPressed_input2 = uia.ClickElementMid(AE_input2);
                                        Thread.Sleep(2000);
                                        IsPressedfunction(isPressed_input2, Convert.ToString(remender_digit2));
                                        int_input2_positive = Convert.ToInt32(Math.Floor(int_input2_positive % (Math.Pow(10, digitsInInput2 - 1))));
                                        digitsInInput2--;
                                    }
                                    if (is_negative2)
                                    {
                                        isPressed_negative2 = uia.ClickElementMid(AE_Negate);
                                        IsPressedfunction(isPressed_negative1, " NEGATION ");
                                    }                                   

                                    CaptureMyScreen(j++);
                                    Console.WriteLine(@"CLICKING '=' !!");
                                    Thread.Sleep(1000);
                                    bool isPressed_equal = false;
                                    isPressed_equal = uia.ClickElementMid(AE_equal_to);
                                    IsPressedfunction(isPressed_equal, Equal_to);
                                    Console.WriteLine("CLICKING ScreenShot of whole!!");
                                    Thread.Sleep(1000);
                                    CaptureMyScreen(j++);                                    
                                }
                            }
                            else
                                Console.WriteLine("AE_Numpad is null");
                        }
                        else
                            Console.WriteLine("AE_Calc is null");
                    }
                    else
                    {
                        Console.WriteLine("Calculator is not opened or not working .");
                    }
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {               
                Console.WriteLine("After Performing Operation ..Closing the Calculator !! . IT WILL CLOSE AFTER 5sec.");
                Thread.Sleep(5000);
                if (!is_Cal_already_opened)
                {
                    Command.KillProcess("Calculator");                    
                }
                else
                {
                    AutomationElement AE_Clear = uia.GetElementById(AutomationElement.RootElement, "clearButton", true);
                    Thread.Sleep(2000);
                    bool isPressed_clear = uia.ClickElementMid(AE_Clear);
                    IsPressedfunction(isPressed_clear, "Clear");
                }
            }
        }


        public static void CaptureMyScreen(int j)
        {
            try
            {
                int k = j;
                Rectangle captureRectangle = System.Windows.Forms.Screen.AllScreens[0].Bounds;
                using (Bitmap captureBitmap = new Bitmap(captureRectangle.Width, captureRectangle.Height, PixelFormat.Format32bppArgb))
                {
                    using (Graphics captureGraphics = Graphics.FromImage(captureBitmap))
                    {
                        captureGraphics.CopyFromScreen(captureRectangle.Left, captureRectangle.Top, 0, 0, captureRectangle.Size);
                    }
                    string path_end = ("capture" + k++ + ".jpg");
                    string path = @"\\pu-cdot02-corp01\atpdatapune\atp\Abhishek_intern2021july\" + path_end;
                    captureBitmap.Save(path, ImageFormat.Jpeg);
                    Console.WriteLine("Screen Captured and Successfully stored at {0}!!\n\n", path);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("CISCO VPN IS CLOSED.Please open\n");

            }
        }

        public static string Coversion_sign_to_String(string variable)
        {
            try
            {
                string buffer_sign;
                switch (variable)
                {
                    case "+":
                        Console.WriteLine("Case +");
                        buffer_sign = "plus";
                        break;

                    case "-":
                        Console.WriteLine("Case -");
                        buffer_sign = "minus";
                        break;

                    case "*":
                        Console.WriteLine("Case *");
                        buffer_sign = "multiply";
                        break;

                    case "/":
                        Console.WriteLine("Case /");
                        buffer_sign = "divide";
                        break;

                    case "=":
                        Console.WriteLine("Case =");
                        buffer_sign = "equal";
                        break;

                    default:
                        Console.WriteLine("The wrong input sign.");
                        buffer_sign = "";
                        break;
                }
                return buffer_sign;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
