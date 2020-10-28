If on VM or if possible run:
sudo apt-get update
sudo apt-get upgrade
sudo apt-get install mono-complete

Compile with:
mcs *.cs -r:System.Windows.Forms.dll -r:System.Drawing.dll
Run with:
mono HangmanGUI.exe
