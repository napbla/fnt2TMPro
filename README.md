# fnt2TMPro
Unity tool to convert Bitmap Font to Text Mesh Pro asset

## Install
Remember to have TextMeshPro imported into your project.
Download and import all files into your Unity project.

## Usage

### 1. Create a dummy TMPro font asset

In Unity, go to **Windows\TextMeshPro\Font Asset Creator**. Fill in these settings as below

![font_asset_creator](https://github.com/napbla/fnt2TMPro/blob/readme/images/font_asset_creator.png?raw=true)

>**Source font file**: choose a true type font that has all your characters in bitmap font. For example Lato, Arial...

>**Sampling font file**: look at your bitmap font description file (.fnt or .txt), it is the size="" part. It you don't know it just leave it there and the script will patch it automatically.
![fnt file with size part](https://github.com/napbla/fnt2TMPro/blob/readme/images/fnt_size.png?raw=true)

>**Padding**: 1 because we don't want to miss any characters in the atlas

>**Packing Method**: **Fast** (If TMPro can not pack all the characters but your bitmap font can then choose "Optimum")

>**Atlas Resolution**: Look at your bitmap font texture and fill in.

>**Character Set**: **Custom Character**, fill in your **Custom Character List** all the characters appeared in your bitmap font

>**Render Mode**: **RASTER** 

Press **Generate Font Atlas** and **Save as...** to your desired font name.

### 2. Patch this dummy font by your bitmap font

Goto **Windows\Bitmap Font Converter**

![bmp font converter](https://github.com/napbla/fnt2TMPro/blob/readme/images/bmp_font_converter.png?raw=true)

Drag your bitmap font texture to **Font Texture** field

Drag your text file or fnt file to **Source Font File** field

Drag your dummy TMPro font asset that you have just created in step 1 to **Destination Font File** field.

Press **Convert** . Now your dummy font is your bitmap font.

## Q&A
1. What if I do not have the fnt or txt file ?

>Use ShoeBox , it's free : https://renderhjs.net/shoebox/

2. How to support this project ?

>You can support me by doing any of these things

* Star this project

* Report bugs or fix and create a pull request
