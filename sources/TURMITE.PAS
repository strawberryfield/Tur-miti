program turmite;

(* --------------------------------------------------------------------------
                     IL PIANO DELLE TURMITI  ver 1.00
                     --------------------------------

         per maggiori informazioni cfr. Le Scienze #254 ottobre 89

         (c) copyright 1989  Roberto Ceccarelli
  -------------------------------------------------------------------------- *)

uses graph,
(*$ifdef bgilink*)
     bgilink,
(*$endif*)
     crt;

const maxstati = 10;
      maxcolori = 16;

type action = record
              nextcolor,nextstatus : integer;
              dir : (sinistra,destra,avanti,indietro);
              end;

var nx,ny,xc,yc,colore,stato,dirx,diry : integer;
    tabella : array[1..maxstati,0..maxcolori] of action;

(* --------------------------------------------------------------------------
   Il programma pu• essere compilato in due versioni:

   1) Definendo il parametro TABFILE si ottiene il programma che carica
      la tabella di transizione da un file ASCII esterno

   2) Se TABFILE non Š definito viene usata una tabella a schema fisso
      incorporata nel codice del programma

   per l'uso dei due programmi cfr. documentazione allegata
---------------------------------------------------------------------------- *)

(*$ifdef tabfile*)
(*$i tabfile.pas*)
(*$else*)
(*$i fissa.pas*)
(*$endif*)

procedure startgraph;
(*
   inizializzazione del modo grafico con riconoscimento automatico della
   scheda grafica presente e rilevazione della risoluzione ammessa

   il driver BGI relativo alla scheda deve essere nella directory corrente,
   in alternativa pu• essere incluso nel programma linkando anche
   la unit BGILINK fornita con il compilatore
   (si pu• ottenere definendo il parametro BGILINK)
*)
var driver,mode : integer;
begin
driver := detect;
initgraph(driver,mode,'');
nx := getmaxx;
ny := getmaxy;
end;

procedure exc(var x,y : integer);
(*
   scambia i due parametri
*)
var tmp : integer;
begin
tmp := x; x := y; y := tmp;
end;

procedure chs(var x : integer);
(*
   cambia il segno al parametro
*)
begin
x := -x;
end;

procedure bound(var x : integer; b : integer);
(*
   verifica che la coordinata sia nel range ammissibile, altrimenti
   considera lo schermo circolare e la scala di conseguenza
*)
begin
if x < 0 then x := x + b;
if x > b then x := x - b;
end;

(*
       MAIN PROGRAM
*)

begin
startgraph;
gettabella;     (* questa funzione Š contenuta nei moduli da includere *)
stato := 1;
dirx := 0; diry := 1;
(*
   ciclo principale del programma
*)
repeat
  colore := getpixel(xc,yc);
  with tabella[stato,colore] do begin
    stato := nextstatus;
    putpixel(xc,yc,nextcolor);
    case dir of
      indietro : begin
                 chs(dirx); chs(diry);
                 end;
      destra   : begin
                 exc(dirx,diry);
                 if dirx <> 0 then chs(dirx);
                 end;
      sinistra : begin
                 exc(dirx,diry);
                 if diry <> 0 then chs(diry);
                 end;
      end;
    xc := xc + dirx; bound(xc,nx);
    yc := yc + diry; bound(yc,ny);
    end;
(*
   il programma si ferma con la pressione di un tasto
*)
  until keypressed;
(*
   la pressione del RETURN ci riporta in modo testo
*)
readln;
restorecrtmode;
end.


