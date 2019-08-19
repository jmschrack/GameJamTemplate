# GameJamTemplate
A basic, pre-set up project with some helpful scripts.

Scripts were designed ease of use in mind for a weekend hackathon/gamejam. 
There were not designed for performance or production use.

## User Settings File
SimplePrefs.cs  will set up a basic dictionary of key/values that can be read and written from a file.
The sample MainMenu scene has a Settings mneu that will auto populate whatever settings it finds. Any value the user changes will be written to disk.

##GameManager
GameManager is a toolbox pattern Singleton. It has a basic Global Vars dictionary of <string,string>.   
Reminder: strings are the devil.  But this is for a hackathon/gamejam, and if you think you aren't gonna bargain with the devil... don't worry.... you will be.