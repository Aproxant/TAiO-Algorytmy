To run the application double click on the .exe file.
In the console you must provide path to example file with two graphs. The files should be in format:

Size of graph A
<Adjacency matrix A>
Size of graph B
<Adjacency matrix B>

example:
4  <- Size A
0,1,1,0
1,0,1,1    <- Adjacency matrix A
1,1,0,0   
0,1,0,0
4  <- Size B
0,1,1,0
1,0,0,1    <- Adjacency matrix B
1,0,0,1 
0,1,1,0


To compile the program you must run the following command which will return the single exe file with all libraries linked to it inside.

dotnet publish -r win-x64 --self-contained true -p:PublishSingleFile=true -p:IncludeAllContentForSelfExtract=true
