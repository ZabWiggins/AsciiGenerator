using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;


namespace AsciiGenerator_2_20_23
{
    class BitmapAscii 
    {
        string grayString = "";
        string grayAscii= "";
        public int userHeight, userWidth, kernelHeight, kernelWidth;
        
        //constructor
        public BitmapAscii(Bitmap userImage)
        {
            userHeight = userImage.Height; userWidth = userImage.Width;  
        }

        //methods


        public string Asciitize(Bitmap userImage)
        {
            int up_down = 0 , left_right = 0; //starting position for x and y axis

            for (up_down = 0; up_down + kernelHeight <= userHeight;) //loops through each row of adding the ascii char per kernel in image
            {
                for(left_right = 0; left_right  + kernelWidth <= userWidth;) 
                {
                    grayString += GrayToString(ScanKernel(userImage, left_right, up_down)); //adds the ascii char for the kernel to the string to output
                    left_right += kernelWidth; //moves to next kernel in the row
                }
                left_right = 0; //resets to the beginning kernel of the row
                up_down += kernelHeight;//moves to next row
                grayString += "\n";//adds new line to string
                if (grayString.Length == (userHeight * userWidth) / (kernelHeight * kernelWidth))
                {
                    break;
                }
            
            }
            return ToString();
        }
        
        double AveragePixel(int R, int G, int B) // converts pixel color components to normalized 0-1 grayscale value
        {
            int grey = (int)(R * 0.3 + G * 0.59 + B * 0.11); 
            double averagePixel = grey / 255; 
            return averagePixel; 
        } 
        //overloaded method
        double AveragePixel(Color pixelColor) //converts color instance components to normalized 0-1 grayscale value
        {
            double averagePixel = (pixelColor.R * 0.3 + pixelColor.G * 0.59 + pixelColor.B * 0.11) / 255;
            return averagePixel; 
        } 
        double AverageColor(List<Color> colorList) //converts list of colors into a single normalized 0-1 grayscale value
        {
            double _averageColor = 0;
            for (int i = 0; i < colorList.Count; i++)
            {
                _averageColor += (colorList[i].R * 0.3 + colorList[i].G * 0.59 + colorList[i].B * 0.11) / 255;
            }
            return _averageColor / colorList.Count;
        }
        string GrayToString(double grayValue) //compares normalzed gray value to ascii chart and returns the corrisponding char
        {
            
            if (grayValue <= .1)
            {
                grayAscii = "@";
            }
            else if (grayValue > .1 && grayValue <= .2)
            {
                grayAscii = "%";
            }
            else if (grayValue > .2 && grayValue <= .3)
            {
                grayAscii = "#";
            }
            else if (grayValue > .3 && grayValue <= .4)
            {
                grayAscii = "*";
            }
            else if (grayValue > .4 && grayValue <= .5)
            {
                grayAscii = "+";
            }
            else if (grayValue > .5 && grayValue <=.6)
            {
                grayAscii = "=";
            }
            else if (grayValue > .6 && grayValue <= .7)
            {
                grayAscii = "-";
            }
            else if (grayValue > .7 && grayValue <= .8)
            {
                grayAscii = ":";
            }
            else if (grayValue > .8 && grayValue <= .9)
            {
                grayAscii = ".";
            }
            else if (grayValue > .9 && grayValue <= 1)
            {
                grayAscii = " ";
            }
            return grayAscii;
        }

        public double ScanKernel(Bitmap userImage, int left_right, int up_down) //calculates the average gray color of a kernel && returns the normalized value
        {
            int i = 0, j = 0; //starting positions within the kernel
            //variables
            double grayValue = 0;
            double kernelGray = 0;
            Color pixelColor;
            
            for( i = 0; i != kernelHeight;)//outer loop for height - repeats until all rows are scanned
            {
                for(j = 0; j != kernelWidth;) //inner loop calculates the gray value of each pixel in a row
                {
                    pixelColor = (userImage.GetPixel(left_right + j, up_down + i)); 
                    grayValue += AveragePixel(pixelColor);
                    j++;
                }
                j = 0; //reset width to start at the beginning of each row
                i++;// moves to next row of kernel if applicable
            }
            kernelGray = grayValue / (kernelHeight * kernelWidth); //calculates average grayscale color of the kernel
            return kernelGray;
        }
        public  override string ToString() //returns the ascii image formatted as a string
        {
            string AsciiImage = grayString;
            return AsciiImage;
        }
    }      
}

    
  
