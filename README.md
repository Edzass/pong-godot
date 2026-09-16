
|                  |                   |                 |
|        Scene     |                   |  Inspector      |
|                  |     Viewport      |                 |
|                  |                   |                 |
|------------------|                   |                 |
|                  |                   |                 |
|     FileSystem   |                   |                 |
|                  |                   |                 |
 --------------------------------------------------------
				   Output                            
									 
Node 2D ir objekta iesatījums pozīcijai, rotācijai un mēroga attēlošanai 2D telpā. 


Scena ir kā mezglu apkopojums, mezgli padara aritektūrtu vieglāk pārredzamu un uztveramu.

dotnet versija --10.0.401

Hello.cs un Hello nosaukumiem ir jāsakrīt, lai Godot spētu automātiski atrast un sasaitīt C# klasi ar mezglu.

Izdzēšot semikolu pēc GD.Print(...) parādās kļūda "error CS1002: ; expected".

| Python   | C#		 |
| -------- | ------- |
| print("Edgars") | Console.WriteLine("Sveiki!");   |
| x = 5 | int x = 5;     |
| komandas beigās nekas nav  | komanda beigās jāraksta semikols    |

### atšķirība
C# ir jāpievieno semikols komandas beigās.

Nomainot paddle objekta Y vērtību uz "0", tā atrašanās vieta izmainās - tuvāk ekrāna augšējai malai.

Logā 1920x1080 centra pozīcija ir (960, 540), tomēr tas ir pie nosacījuma, ja visas x un y vērtībās ir pozitīvas. Manā gadījumā Godot vidē daļa no vērtībām ir negatīvas, jo noklusējuma kamera tā ir iestatīta.