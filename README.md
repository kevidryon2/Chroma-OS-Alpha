# Chroma OS
A little OS i made using COSMOS for C#.

## How to run it
To run Chroma OS on a virtual machine, just use VMWare or any other VM Software. If instead you wanna run it on real hardware:

1. Use a tool like Rufus or DD to masterize the ISO file on a USB or CD.
2. Just connect the USB to the computer of choice and run it through the BIOS.

### Building from source
Because it took me an entire day to write this, i forgot to export the Release ISO, but instead exported the Debug one. So, i guess you're gonna have to build Chroma OS from source. No program though. If you want to, follow these steps:

1. Download and install all the required software for [COSMOS](https://www.gocosmos.org/docs/install/ "COSMOS") and COSMOS itself.
2. Download the source code of Chroma OS.
3. Create a new COSMOS C# kernel project on Visual Studio.
4. Replace the files of the new project with the files of Chroma OS, except the /bin/ directory.
5. Run the project.