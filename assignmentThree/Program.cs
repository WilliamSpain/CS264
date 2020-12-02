using System;
using System.IO;
using System.Collections.Generic;
using System.Reflection;


namespace assignmentThree
{
    public class Shape{
        public string x{get;set;}
        public string y{get;set;}
    }
    public class Rectangle : Shape{
       
        public string width{get;set;}
        public string height{get;set;}
        public string stroke {get;set;}
        public string strokeWidth{get;set;}
        public string fill{get;set;}
        public Rectangle(){x="0";y="0";width="100";height="100"; stroke="black";strokeWidth="2";fill="grey";}
        //public Rectangle(string X,string Y,string Width,string Height){x=X;y=Y;width=Width;height=Height;}
        public Rectangle(string X,string Y,string Width,string Height,string Stroke,string StrokeWidth,string Fill){x=X;y=Y;width=Width;height=Height;stroke=Stroke;strokeWidth=StrokeWidth;fill=Fill;}
    }
    public class Circle:Shape{
        public string Radius{get;set;}
        public string stroke {get;set;}
        public string strokeWidth{get;set;}
        public string fill{get;set;}
        public Circle(){x="0";y="0";Radius="5";stroke="black";strokeWidth="2";fill="blue";}
      //  public Circle(string X,string Y,string radius){x=X;y=Y;Radius=radius;}
        public Circle(string X,string Y,string radius,string Stroke,string StrokeWidth,string Fill){x=X;y=Y;Radius=radius;stroke=Stroke;strokeWidth=StrokeWidth;fill=Fill;}
        
    }
    public class Ellipse:Shape{
        public string XRadius{get;set;}
        public string YRadius{get;set;}
        public string stroke {get;set;}
        public string strokeWidth{get;set;}
        public string fill{get;set;}
        public Ellipse(){x="0";y="0";XRadius="5";YRadius="5";stroke="black";strokeWidth="2";fill="purple";}
        //public Ellipse(string X,string Y,string xradius,string yradius){x=X;y=Y;XRadius=xradius;YRadius=yradius;}
        public Ellipse(string X,string Y,string xradius,string yradius,string Stroke,string StrokeWidth,string Fill){x=X;y=Y;XRadius=xradius;YRadius=yradius;stroke=Stroke;strokeWidth=StrokeWidth;fill=Fill;}
    }
    public class Line:Shape{
        public string XTwo{get;set;}
        public string YTwo{get;set;}
        public string stroke {get;set;}
        public string strokeWidth{get;set;}
        public Line(){x="0";y="0";XTwo="5";YTwo="5";stroke="black";strokeWidth="2";}
        //public Line(string X,string Y,string xtwo,string ytwo){x=X;y=Y;XTwo=xtwo;YTwo=ytwo;}
        public Line(string X,string Y,string xtwo,string ytwo,string Stroke,string StrokeWidth){x=X;y=Y;XTwo=xtwo;YTwo=ytwo;stroke=Stroke;strokeWidth=StrokeWidth;}

    }
    public class Polyline:Shape{
        public string points{get;set;}
        public string stroke {get;set;}
        public string strokeWidth{get;set;}
        public Polyline(){points="0,0";stroke="black";strokeWidth="2";}
        //public Polyline(string Points){points=Points;}
        public Polyline(string Points,string Stroke,string StrokeWidth){points=Points;stroke=Stroke;strokeWidth=StrokeWidth;}
    }
    public class Polygon:Shape{
        public string points{get;set;}
        public string stroke {get;set;}
        public string strokeWidth{get;set;}
        public string fill{get;set;}
        public Polygon(){points="0,0";stroke="black";strokeWidth="2";fill="yellow";}
        public Polygon(string Points,string Stroke,string StrokeWidth,string Fill){points=Points;stroke=Stroke;strokeWidth=StrokeWidth;fill=Fill;}
        //public Polygon(string Points){points=Points;}
    }
    public class Path:Shape{
        public string d{get;set;}
        public string stroke {get;set;}
        public string strokeWidth{get;set;}
        public Path(){d="";stroke="black";strokeWidth="2";}
        //public Path(string D){d=D;Stroke=stroke;StrokeWidth=strokeWidth;}
        public Path(string D,string Stroke,string StrokeWidth){d=D;stroke=Stroke;strokeWidth=StrokeWidth;}
    }

    class Program
    {
        /* ------------------------------------------------------------

                    windows 10 64 bit
                    visual studio code 1.51.1



            -----------------------------------------------------------
        */
        static void Main(string[] args)
        {
            /*
                memento method
                undo is a window to remove tasks
                redo is a window to reinstate tasks
            */
            Stack<Shape> undo=new Stack<Shape>();
            Stack<Shape> redo=new Stack<Shape>();
            
            Random tests =new Random();
            string start=createCanvas();
            List<Shape> myShapes=new List<Shape>();
            bool finished=false;
            string lastCommand="";
            while(!finished){
                Console.WriteLine("------------------");
                Console.WriteLine("Write the option you want to console!");
                Console.WriteLine("To finish and create SVG file write \"done\"");
                Console.WriteLine("1. \"undo\" ");   //removes comepleted tasks
                Console.WriteLine("2. \"redo\" ");  //reinstates tasks
                Console.WriteLine("3. \"create shapes\""); // write shape name
                Console.WriteLine("4. \"delete shapes\""); // delete shape at index
                Console.WriteLine("5. \"shape list\""); //lists all shapes
                Console.WriteLine("6. \"edit coordinates\""); // change shape specific values not style
                Console.WriteLine("7. \"reorder list\"");// change z value
                Console.WriteLine("8. \"edit style\"");// change styles specific stuff
                Console.WriteLine("9. \"clear canvas\"");
                Console.WriteLine("------------------");
                string choice =Console.ReadLine();
                if(choice=="done"){
                    printToSvgFile(myShapes,start);
                    finished=true;
                }
                else if(choice=="undo"){
                    if(lastCommand=="create shapes"&&myShapes.Count>0){
                        redo.Push(myShapes[myShapes.Count-1]);
                        int listLength=myShapes.Count-1;
                        myShapes=deleteShapes(myShapes,listLength.ToString());
                    }
                    else if(lastCommand=="clear canvas"){
                        int undoCounter=undo.Count;
                        for(int i=0;i<undoCounter;i++){
                            myShapes.Add(undo.Pop());
                        }
                    }
                    
                }
                else if(choice=="redo"){
                    if(redo.Count>0&&lastCommand=="create shapes"){
                        myShapes.Add(redo.Pop());
                    }
                    else if(lastCommand=="clear canvas"){
                        int size=myShapes.Count;
                        for(int i=size-1;i>=0;i--){
                            undo.Push(myShapes[i]);
                        }
                        myShapes.Clear();
                    }
                }
                else if(choice=="create shapes"){lastCommand=choice; myShapes=createShapes(myShapes);}
                else if(choice=="delete shapes"){
                    
                    Console.WriteLine("write index to be deleted or \"done\" to cancel");
                    string del=Console.ReadLine();
                    
                    if(del=="done"){break;}
                    else{
                        redo.Push(myShapes[Convert.ToInt32(del)]);
                        myShapes=deleteShapes(myShapes,del);
                    }
                }
                else if(choice=="shape list"){
                    listShapes(myShapes);
                }
                else if(choice=="edit list"){
                    Console.WriteLine("write index to be edited or \"done\" to cancel");
                    string edit=Console.ReadLine();
                    if(edit=="done"){break;}
                    myShapes= editShapes(myShapes,edit);
                }
                else if(choice=="shape style"){
                    Console.WriteLine("write index to have style edited or \"done\" to cancel");
                    string edit=Console.ReadLine();
                    if(edit=="done"){break;}
                    myShapes= shapeStyle(myShapes,edit);
                }
                else if(choice=="reorder list"){
                    Console.WriteLine("write current index of shape or \"done\" to cancel");
                    string currentPosition=Console.ReadLine();
                    if(currentPosition=="done"){break;}
                    System.Console.WriteLine("write desired index of shape");
                    string desiredIndex=Console.ReadLine();
                    myShapes=reorderShapes(myShapes,currentPosition,desiredIndex);
                }
                else if(choice=="clear canvas"){
                    lastCommand=choice;
                    Console.WriteLine("are you sure you want to clear canvas type \"yes\" to confirm or \"no\" to cancel");
                    string clear=Console.ReadLine();
                    if(clear=="yes"){
                        for(int i=myShapes.Count-1;i>=0;i--){
                            undo.Push(myShapes[i]);
                        }
                        myShapes.Clear();
                    }
                }
                
                
            }
            //prints element properties of myshapes to console
            // foreach(var shape in myShapes){
            //     foreach(PropertyInfo prop in shape.GetType().GetProperties()){
            //         Console.WriteLine($"{prop.Name,-20} {prop.GetValue(shape,null)}");
            //     }        
            // }
        }
        public static string createCanvas(){
        //Console.WriteLine("please enter canvas height");
        //string canvasHeight=Console.ReadLine();
        string canvasHeight="500";
        //Console.WriteLine("please enter canvas width");
        //string canvasWidth=Console.ReadLine();
        string canvasWidth="500";
        string start=string.Format(@"<svg width=""{0}"" height=""{1}"" >",canvasWidth,canvasHeight);
        Console.WriteLine("canvas created.");
        return start;    
        }
        public static List<Shape> createShapes(List<Shape> shapeList){
            Console.WriteLine("------------------");
            Console.WriteLine("Write a shape choice to console");
            Console.WriteLine("write \"done\" to console to finish creating shapes");
            Console.WriteLine("1. \"rectangle\"");
            Console.WriteLine("2. \"circle\"");
            Console.WriteLine("3. \"ellipse\"");
            Console.WriteLine("4. \"line\"");
            Console.WriteLine("5. \"polyline\"");
            Console.WriteLine("6. \"polygon\"");
            Console.WriteLine("7. \"path\"");
            Console.WriteLine("------------------");

            string choice=Console.ReadLine();
            if(choice=="rectangle"){shapeList.Add(returnRectangleObject());}
            else if(choice=="circle"){shapeList.Add(returnCircleObject());}
            else if(choice=="ellipse"){shapeList.Add(returnEllipseObject());}
            else if(choice=="line"){shapeList.Add(returnLineObject());}
            else if(choice=="polyline"){shapeList.Add(returnPolylineObject()); }
            else if(choice=="polygon"){shapeList.Add(returnPolygonObject());}
            else if(choice=="path"){shapeList.Add(returnPathObject());}
            else{return shapeList;}

             Console.WriteLine("Shape created"); 
            return shapeList;
        }
        public static List<Shape> deleteShapes(List<Shape> shapeList,string index){
            int number=Convert.ToInt32(index);
            shapeList.RemoveAt(number);
            return shapeList;
        }
        public static void listShapes(List<Shape> shapeList){
            for(int i=0;i<shapeList.Count;i++){
                string str=shapeList[i].ToString();
                string result = str.Substring(str.LastIndexOf('.') + 1);
                if(result=="Circle"){
                    string circleX=((Circle)shapeList[i]).x;
                    string circleY=((Circle)shapeList[i]).y;
                    string circleRadius=((Circle)shapeList[i]).Radius;
                    string circleStroke=((Circle)shapeList[i]).stroke;
                    string circleFill=((Circle)shapeList[i]).fill;
                    string circleSW=((Circle)shapeList[i]).strokeWidth;
                    string cir=$"circle {{x={circleX} y={circleY} r={circleRadius}}}";
                    Console.WriteLine(i+". "+cir);
                }
                else if(result=="Rectangle"){
                    string rectX=((Rectangle)shapeList[i]).x;
                    string rectY=((Rectangle)shapeList[i]).y;
                    string rectWidth=((Rectangle)shapeList[i]).width;
                    string rectHeight=((Rectangle)shapeList[i]).height;
                    string rectStroke=((Rectangle)shapeList[i]).stroke;
                    string rectFill=((Rectangle)shapeList[i]).fill;
                    string rectSW=((Rectangle)shapeList[i]).strokeWidth;
                    string rec=$"rectangle {{x={rectX} y={rectY} width={rectWidth} height={rectHeight}}}";
                    Console.WriteLine(i+". "+rec);
                }
                else if(result=="Ellipse"){
                    string elliX=((Ellipse)shapeList[i]).x;
                    string elliY=((Ellipse)shapeList[i]).y;
                    string elliXR=((Ellipse)shapeList[i]).XRadius;
                    string elliYR=((Ellipse)shapeList[i]).YRadius;
                    string elliStroke=((Ellipse)shapeList[i]).stroke;
                    string elliFill=((Ellipse)shapeList[i]).fill;
                    string elliSW=((Ellipse)shapeList[i]).strokeWidth;
                    string elli=$"ellipse {{x={elliX} y={elliY} rx={elliXR} ry={elliYR}}}";
                    Console.WriteLine(i+". "+elli);

                }
                else if(result=="Line"){
                    string linex1=((Line)shapeList[i]).x;
                    string liney1=((Line)shapeList[i]).y;
                    string linex2=((Line)shapeList[i]).XTwo;
                    string liney2=((Line)shapeList[i]).YTwo;
                    string lineStroke=((Line)shapeList[i]).stroke;
                    string lineSW=((Line)shapeList[i]).strokeWidth;
                    string line=$"line {{x1={linex1} y1={liney1} x2={linex2} y2={liney2}}}";
                    Console.WriteLine(i+". "+line);
                }
                else if(result=="Polyline"){
                    string polylPoints=((Polyline)shapeList[i]).points;
                    string polylStroke=((Polyline)shapeList[i]).stroke;
                    string polylSW=((Polyline)shapeList[i]).strokeWidth;
                    string polyl=$"polyline {{points={polylPoints}}}";
                    Console.WriteLine(i+". "+polyl);
                }
                else if(result=="Polygon"){
                    string polPoints=((Polygon)shapeList[i]).points;
                    string polStroke=((Polygon)shapeList[i]).stroke;
                    string polFill=((Polygon)shapeList[i]).fill;
                    string polSW=((Polygon)shapeList[i]).strokeWidth;
                    string pol=$"polygon {{points={polPoints}}}";
                    Console.WriteLine(i+". "+pol);
                }
                else if(result=="Path"){
                    string patPoints=((Path)shapeList[i]).d;
                    string patStroke=((Path)shapeList[i]).stroke;
                    string patSW=((Path)shapeList[i]).strokeWidth;
                    string pat=$"path {{d={patPoints}}}";
                    Console.WriteLine(i+". "+pat);
                }
                
            }
        }
        public static List<Shape> editShapes(List<Shape> shapeList,string index){
            
            int number=Convert.ToInt32(index);
            string str=shapeList[number].ToString();
            string result = str.Substring(str.LastIndexOf('.') + 1);
            if(result=="Circle"){
                Console.WriteLine($"previous x value={((Circle)shapeList[number]).x} enter new value");
                ((Circle)shapeList[number]).x=Console.ReadLine();
                Console.WriteLine($"previous y value={((Circle)shapeList[number]).y} enter new value");
                ((Circle)shapeList[number]).y=Console.ReadLine();
                Console.WriteLine($"previous radius value={((Circle)shapeList[number]).Radius} enter new value");
                ((Circle)shapeList[number]).Radius=Console.ReadLine();
            }
            else if(result=="Rectangle"){
                Console.WriteLine($"previous x value={((Rectangle)shapeList[number]).x} enter new value");
                ((Rectangle)shapeList[number]).x=Console.ReadLine();
                Console.WriteLine($"previous y value={((Rectangle)shapeList[number]).y} enter new value");
                ((Rectangle)shapeList[number]).y=Console.ReadLine();
                Console.WriteLine($"previous Width value={((Rectangle)shapeList[number]).width} enter new value");
                ((Rectangle)shapeList[number]).width=Console.ReadLine();
                Console.WriteLine($"previous height value={((Rectangle)shapeList[number]).height} enter new value");
                ((Rectangle)shapeList[number]).height=Console.ReadLine();
            }
            else if(result=="Ellipse"){
                Console.WriteLine($"previous x value={((Ellipse)shapeList[number]).x} enter new value");
                ((Ellipse)shapeList[number]).x=Console.ReadLine();
                Console.WriteLine($"previous y value={((Ellipse)shapeList[number]).y} enter new value");
                ((Ellipse)shapeList[number]).y=Console.ReadLine();
                Console.WriteLine($"previous xradius value={((Ellipse)shapeList[number]).XRadius } enter new value");
                ((Ellipse)shapeList[number]).XRadius=Console.ReadLine();
                Console.WriteLine($"previous yradius value={((Ellipse)shapeList[number]).YRadius} enter new value");
                ((Ellipse)shapeList[number]).YRadius=Console.ReadLine();
            }
            else if(result=="Line"){
                Console.WriteLine($"previous x value={((Line)shapeList[number]).x} enter new value");
                ((Line)shapeList[number]).x=Console.ReadLine();
                Console.WriteLine($"previous y value={((Line)shapeList[number]).y} enter new value");
                ((Line)shapeList[number]).y=Console.ReadLine();
                Console.WriteLine($"previous xtwo value={((Line)shapeList[number]).XTwo } enter new value");
                ((Line)shapeList[number]).XTwo=Console.ReadLine();
                Console.WriteLine($"previous ytwo value={((Line)shapeList[number]).YTwo} enter new value");
                ((Line)shapeList[number]).YTwo=Console.ReadLine();
            }
            else if(result=="Polyline"){
                Console.WriteLine($"previous points value={((Polyline)shapeList[number]).points } enter new value");
                ((Polyline)shapeList[number]).points=Console.ReadLine();
            }
            else if(result=="Polygon"){
                Console.WriteLine($"previous points value={((Polygon)shapeList[number]).points } enter new value");
                ((Polygon)shapeList[number]).points=Console.ReadLine();}
            else if(result=="Path"){
                Console.WriteLine($"previous d value={((Path)shapeList[number]).d } enter new value");
                ((Path)shapeList[number]).d=Console.ReadLine();
            }
            return shapeList;   
        }
        public static List<Shape> reorderShapes(List<Shape> shapeList,string currentPos,string desiredPos){
            int current=Convert.ToInt32(currentPos);
            int desired=Convert.ToInt32(desiredPos);
            List<Shape> temp=new List<Shape>();
            temp.AddRange(shapeList);
            temp.RemoveAt(current);
            temp.Insert(desired,shapeList[current]);
            return temp;
        }
        public static List<Shape> shapeStyle(List<Shape> shapeList,string index){
            int number=Convert.ToInt32(index);
            string str=shapeList[number].ToString();
            string result = str.Substring(str.LastIndexOf('.') + 1);
            if(result=="Circle"){
                Console.WriteLine($"previous stroke value={((Circle)shapeList[number]).stroke} enter new value");
                ((Circle)shapeList[number]).stroke=Console.ReadLine();
                Console.WriteLine($"previous stroke width value={((Circle)shapeList[number]).strokeWidth} enter new value");
                ((Circle)shapeList[number]).strokeWidth=Console.ReadLine();
                Console.WriteLine($"previous fill value={((Circle)shapeList[number]).fill} enter new value");
                ((Circle)shapeList[number]).fill=Console.ReadLine();
            }
            else if(result=="Rectangle"){
                Console.WriteLine($"previous stroke value={((Rectangle)shapeList[number]).stroke} enter new value");
                ((Rectangle)shapeList[number]).stroke=Console.ReadLine();
                Console.WriteLine($"previous stroke width value={((Rectangle)shapeList[number]).strokeWidth} enter new value");
                ((Rectangle)shapeList[number]).strokeWidth=Console.ReadLine();
                Console.WriteLine($"previous fill value={((Rectangle)shapeList[number]).fill} enter new value");
                ((Rectangle)shapeList[number]).fill=Console.ReadLine();
            }
            else if(result=="Ellipse"){
                Console.WriteLine($"previous stroke value={((Ellipse)shapeList[number]).stroke} enter new value");
                ((Ellipse)shapeList[number]).stroke=Console.ReadLine();
                Console.WriteLine($"previous stroke width value={((Ellipse)shapeList[number]).strokeWidth} enter new value");
                ((Ellipse)shapeList[number]).strokeWidth=Console.ReadLine();
                Console.WriteLine($"previous fill value={((Ellipse)shapeList[number]).fill} enter new value");
                ((Ellipse)shapeList[number]).fill=Console.ReadLine();
            }
            else if(result=="Line"){
                Console.WriteLine($"previous stroke value={((Line)shapeList[number]).stroke} enter new value");
                ((Line)shapeList[number]).stroke=Console.ReadLine();
                Console.WriteLine($"previous stroke width value={((Line)shapeList[number]).strokeWidth} enter new value");
                ((Line)shapeList[number]).strokeWidth=Console.ReadLine();
            }
            else if(result=="Polyline"){
                Console.WriteLine($"previous stroke value={((Polyline)shapeList[number]).stroke} enter new value");
                ((Polyline)shapeList[number]).stroke=Console.ReadLine();
                Console.WriteLine($"previous stroke width value={((Polyline)shapeList[number]).strokeWidth} enter new value");
                ((Polyline)shapeList[number]).strokeWidth=Console.ReadLine();
            }
            else if(result=="Polygon"){
                Console.WriteLine($"previous stroke value={((Polygon)shapeList[number]).stroke } enter new value");
                ((Polygon)shapeList[number]).stroke=Console.ReadLine();
                Console.WriteLine($"previous stroke width value={((Polygon)shapeList[number]).strokeWidth} enter new value");
                ((Polygon)shapeList[number]).strokeWidth=Console.ReadLine();
                Console.WriteLine($"previous fill  value={((Polygon)shapeList[number]).fill} enter new value");
                ((Polygon)shapeList[number]).fill=Console.ReadLine();
            }
            else if(result=="Path"){
                Console.WriteLine($"previous stroke value={((Path)shapeList[number]).stroke} enter new value");
                ((Path)shapeList[number]).stroke=Console.ReadLine();
                Console.WriteLine($"previous stroke width value={((Path)shapeList[number]).strokeWidth} enter new value");
                ((Path)shapeList[number]).strokeWidth=Console.ReadLine();
            }
            return shapeList;   
        }
        public static void printToSvgFile(List<Shape> shapeList,string start){
            String path=@"E:\Users\William\Desktop\MYAPP\assignmentThree\test.txt";
            string end=@"<svg/>"; 
            File.WriteAllText(path,start+Environment.NewLine);
            for(int i=0;i<shapeList.Count;i++){
                string str=shapeList[i].ToString();
                string result = str.Substring(str.LastIndexOf('.') + 1);
                if(result=="Circle"){
                    string circleX=((Circle)shapeList[i]).x;
                    string circleY=((Circle)shapeList[i]).y;
                    string circleRadius=((Circle)shapeList[i]).Radius;
                    string circleStroke=((Circle)shapeList[i]).stroke;
                    string circleFill=((Circle)shapeList[i]).fill;
                    string circleSW=((Circle)shapeList[i]).strokeWidth;
                    string cir=$"<circle cx=\"{circleX}\" cy=\"{circleY}\" r=\"{circleRadius}\" stroke=\"{circleStroke}\" fill=\"{circleFill}\" stroke-width=\"{circleSW}\"/>";
                    File.AppendAllText(path,cir+Environment.NewLine);
                }
                else if(result=="Rectangle"){
                    string rectX=((Rectangle)shapeList[i]).x;
                    string rectY=((Rectangle)shapeList[i]).y;
                    string rectWidth=((Rectangle)shapeList[i]).width;
                    string rectHeight=((Rectangle)shapeList[i]).height;
                    string rectStroke=((Rectangle)shapeList[i]).stroke;
                    string rectFill=((Rectangle)shapeList[i]).fill;
                    string rectSW=((Rectangle)shapeList[i]).strokeWidth;
                    string rec=$"<rect x=\"{rectX}\" y=\"{rectY}\" width=\"{rectWidth}\" height=\"{rectHeight}\" stroke=\"{rectStroke}\" fill=\"{rectStroke}\" stroke-width=\"{rectSW}\"/>";
                    File.AppendAllText(path,rec+Environment.NewLine);
                }
                else if(result=="Ellipse"){
                    string elliX=((Ellipse)shapeList[i]).x;
                    string elliY=((Ellipse)shapeList[i]).y;
                    string elliXR=((Ellipse)shapeList[i]).XRadius;
                    string elliYR=((Ellipse)shapeList[i]).YRadius;
                    string elliStroke=((Ellipse)shapeList[i]).stroke;
                    string elliFill=((Ellipse)shapeList[i]).fill;
                    string elliSW=((Ellipse)shapeList[i]).strokeWidth;
                    string elli=$"<ellipse cx=\"{elliX}\" cy=\"{elliY}\" rx=\"{elliXR}\" ry=\"{elliYR}\" stroke=\"{elliStroke}\" fill=\"{elliFill}\" stroke-width=\"{elliSW}\"/>";
                    File.AppendAllText(path,elli+Environment.NewLine);

                }
                else if(result=="Line"){
                    string linex1=((Line)shapeList[i]).x;
                    string liney1=((Line)shapeList[i]).y;
                    string linex2=((Line)shapeList[i]).x;
                    string liney2=((Line)shapeList[i]).y;
                    string lineStroke=((Line)shapeList[i]).stroke;
                    string lineSW=((Line)shapeList[i]).strokeWidth;
                    string line=$"<line x1=\"{linex1}\" y1=\"{liney1}\" x2=\"{linex2}\" y2=\"{liney2}\" stroke=\"{lineStroke}\" stroke-width=\"{lineSW}\"/>";
                    File.AppendAllText(path,line+Environment.NewLine);
                }
                else if(result=="Polyline"){
                    string polylPoints=((Polyline)shapeList[i]).points;
                    string polylStroke=((Polyline)shapeList[i]).stroke;
                    string polylSW=((Polyline)shapeList[i]).strokeWidth;
                    string polyl=$"<polyline points=\"{polylPoints}\" fill=\"none\" stroke=\"{polylStroke}\" stroke-width=\"{polylSW}\"/>";
                    File.AppendAllText(path,polyl+Environment.NewLine);
                }
                else if(result=="Polygon"){
                    string polPoints=((Polygon)shapeList[i]).points;
                    string polStroke=((Polygon)shapeList[i]).stroke;
                    string polFill=((Polygon)shapeList[i]).fill;
                    string polSW=((Polygon)shapeList[i]).strokeWidth;
                    string pol=$"<polygon points=\"{polPoints}\" stroke=\"{polStroke}\" fill=\"{polFill}\" stroke-width=\"{polSW}\"/>";
                    File.AppendAllText(path,pol+Environment.NewLine);
                }
                else if(result=="Path"){
                    string patPoints=((Path)shapeList[i]).d;
                    string patStroke=((Path)shapeList[i]).stroke;
                    string patSW=((Path)shapeList[i]).strokeWidth;
                    string pat=$"<path d=\"{patPoints}\" stroke=\"{patStroke}\" stroke-width=\"{patSW}\"/>";
                    File.AppendAllText(path,pat+Environment.NewLine);
                }
                
            }
            File.AppendAllText(path,end+Environment.NewLine);
            File.Move(path,"test.svg");
            
        }
        public static Rectangle returnRectangleObject(){
            Random rnd =new Random();
            //Console.WriteLine("Please enter rectangle x location"); 
            //string userX=Console.ReadLine();
            string userX=rnd.Next(400).ToString(); 
            //Console.WriteLine("Please enter rectangle y location");
            //string userY=Console.ReadLine();
            string userY=rnd.Next(400).ToString();
            //Console.WriteLine("Please enter rectangle width"); 
            //string userWidth=Console.ReadLine();
            string userWidth=rnd.Next(100).ToString();
           // Console.WriteLine("Please enter rectangle height");
            //string userHeight=Console.ReadLine();
            string userHeight=rnd.Next(100).ToString();
            //Console.WriteLine("Please enter rectangle stroke");
            string stroke="black";
//Console.WriteLine("Please enter rectangle stroke width");
            //string strokewidth=Console.ReadLine();
            string strokewidth="1";
          //  Console.WriteLine("Please enter rectangle fill");
            //string fill=Console.ReadLine();
            string fill="black";
            
            return new Rectangle(userX,userY,userHeight,userWidth,stroke,strokewidth,fill);
        }
        public static Circle returnCircleObject(){
            Random rnd=new Random();
           // Console.WriteLine("write circle x location");
            //string x=Console.ReadLine();
            string x=rnd.Next(400).ToString(); 
           // Console.WriteLine("write circle y location");
            //string y=Console.ReadLine();
            string y=rnd.Next(400).ToString(); 
           // Console.WriteLine("write radius of circle");
            //string radius=Console.ReadLine();
            string radius=rnd.Next(75).ToString(); 
            //Console.WriteLine("Please enter circle stroke");
            string stroke="black";
           // Console.WriteLine("Please enter circle stroke width");
            //string strokewidth=Console.ReadLine();
            string strokewidth="1";
            //Console.WriteLine("Please enter circle fill");
            //string fill=Console.ReadLine();
            string fill="black";
            return new Circle(x,y,radius,stroke,strokewidth,fill);   
        }
        public static Ellipse returnEllipseObject(){
            Random rnd=new Random();
            //Console.WriteLine("write ellipse x location");
            //string x=Console.ReadLine();
            string x=rnd.Next(400).ToString();
            //Console.WriteLine("write ellipse y location");
            //string y=Console.ReadLine();
            string y=rnd.Next(400).ToString();
            //Console.WriteLine("write x radius of ellipse");
            //string Xradius=Console.ReadLine();
            string Xradius=rnd.Next(50,101).ToString();
            //Console.WriteLine("write y radius of ellipse");
            //string Yradius=Console.ReadLine();
            string Yradius=rnd.Next(10,40).ToString();
            //Console.WriteLine("Please enter ellipse stroke");
            //string stroke=Console.ReadLine();
            string stroke="black";
           // Console.WriteLine("Please enter ellipse stroke width");
            //string strokewidth=Console.ReadLine();
            string strokewidth="1";
           // Console.WriteLine("Please enter ellipse fill");
            //string fill=Console.ReadLine();
            string fill="black";
            return new Ellipse(x,y,Xradius,Yradius,stroke,strokewidth,fill);
        }
        public static Line returnLineObject(){
            Random rnd=new Random();
            //Console.WriteLine("write line x1 location");
            //string x=Console.ReadLine();
            string x=rnd.Next(400).ToString();
            //Console.WriteLine("write line y1 location");
            //string y=Console.ReadLine();
            string y=rnd.Next(400).ToString();
            //Console.WriteLine("write line x2 location");
            //string XTwo=Console.ReadLine();
            string XTwo=rnd.Next(400).ToString();
            //Console.WriteLine("write ellipse y2 location");
            //string YTwo=Console.ReadLine();
            string YTwo=rnd.Next(400).ToString();
            //Console.WriteLine("Please enter line stroke");
            //string stroke=Console.ReadLine();
            string stroke="black";
            //Console.WriteLine("Please enter line stroke width");
            //string strokewidth=Console.ReadLine();
            string strokewidth="1";
            return new Line(x,y,XTwo,YTwo,stroke,strokewidth);
        }
        public static Polyline returnPolylineObject(){
            Random rnd=new Random();
            string points="";
            int counter=rnd.Next(2,10);
            for(int i=0;i<counter;i++){
                string pointOne=rnd.Next(400).ToString();
                points+=pointOne+",";
                string pointTwo=rnd.Next(400).ToString();
                points+=pointTwo+" ";
            }

            // bool creatingPoints=false;
            // int counter=1;
            // while(!creatingPoints){
            //     Console.WriteLine($"write x{counter} value or write \"done\" to stop adding points");
            //     pointOne=Console.ReadLine();
            //     if(pointOne=="done"){creatingPoints=true; break;}
            //     Console.WriteLine($"write y{counter} value");
            //     pointTwo=Console.ReadLine();
            //     temp.Add($"{pointOne},{pointTwo}");
            //     counter++;
            // }
            // string points="";
            // foreach(var point in temp){
            //     points+=point+" ";
            // }
            //Console.WriteLine("Please enter Polyline stroke");
            //string stroke=Console.ReadLine();
            string stroke="black";
            //Console.WriteLine("Please enter Polyline stroke width");
            //string strokewidth=Console.ReadLine();
            string strokewidth="1";
            return new Polyline(points,stroke,strokewidth);
        }
        public static Polygon returnPolygonObject(){
            // List<string> temp=new List<string>();
            // bool creatingPoints=false;
            // int counter=1;
            // string pointOne;
            // string pointTwo;
            // while(!creatingPoints){
            //     Console.WriteLine($"write x{counter} value or write \"done\" to stop adding points");
            //     pointOne=Console.ReadLine();
            //     if(pointOne=="done"){creatingPoints=true; break;}
            //     Console.WriteLine($"write y{counter} value");
            //     pointTwo=Console.ReadLine();
            //     temp.Add($"{pointOne},{pointTwo}");
            //     counter++;
            // }
            // string points="";
            // foreach(var point in temp){
            //     points+=point+" ";
            // }
            Random rnd=new Random();
            string points="";
            int counter=rnd.Next(1,6);
            for(int i=0;i<counter;i++){
                string pointOne=rnd.Next(400).ToString();
                points+=pointOne+",";
                string pointTwo=rnd.Next(400).ToString();
                points+=pointTwo+" ";
            }
            //Console.WriteLine("Please enter Polygon stroke");
            //string stroke=Console.ReadLine();
            string stroke="black";
            //Console.WriteLine("Please enter Polygon stroke width");
            //string strokewidth=Console.ReadLine();
            string strokewidth="1";
            //Console.WriteLine("Please enter Polygon fill");
            //string fill=Console.ReadLine();
            string fill="black";
            return new Polygon(points,stroke,strokewidth,fill);
        }
        public static Path returnPathObject(){
            // List<string> temp=new List<string>();
            // bool creatingPoints=false;
            // string key;
            // string pointOne;
            // string pointTwo;
            // while(!creatingPoints){    
            //     Console.WriteLine("write key letter or write \"done\" to stop adding points");
            //     key=Console.ReadLine();
            //     if(key=="done"){creatingPoints=true; break;}
            //     else if(key=="z"||key=="Z"){temp.Add($"{key}");}
            //     else if(key=="H"||key=="h"){
            //         System.Console.WriteLine("write horizontal value");
            //         pointOne=Console.ReadLine();
            //         temp.Add($"{key} {pointOne}");
            //     }
            //     else if(key=="V"||key=="v"){
            //         System.Console.WriteLine("write vertical value");
            //         pointOne=Console.ReadLine();
            //         temp.Add($"{key} {pointOne}");
            //     }
            //     else{
            //         System.Console.WriteLine("write x value");
            //         pointOne=Console.ReadLine();
            //         System.Console.WriteLine("write y value");
            //         pointTwo=Console.ReadLine();
            //         temp.Add($"{key} {pointOne} {pointTwo}");
            //     }
            // }

            // string points="";
            // foreach(var point in temp){
            //     points+=point+" ";
            // }

            Random rnd=new Random();
            string points="";
            int counter=rnd.Next(1,5);
            for(int i=0;i<counter;i++){
                string pointOne=rnd.Next(400).ToString();
                points+="M "+pointOne+" ";
                string pointTwo=rnd.Next(400).ToString();
                points+=pointTwo+" ";
            }
            int choice=rnd.Next(0,3);
            if(choice==0){
                string vert=rnd.Next(100).ToString();
                points+="V "+vert;
            }
            else if(choice==1){
                string horiz=rnd.Next(100).ToString();
                points+="H "+horiz;
            }
            else{points+="Z";}
           // Console.WriteLine("Please enter Path stroke");
            //string stroke=Console.ReadLine();
            string stroke="black";
            //Console.WriteLine("Please enter Path stroke width");
            //string strokewidth=Console.ReadLine();
            string strokewidth="1";
            //Console.WriteLine(points);
            return new Path(points,stroke,strokewidth);

        }
    }
}
