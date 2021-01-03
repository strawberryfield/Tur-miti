(* --------------------------------------------------------------------------
      Modulo per la lettura delle tabelle di TURMITE 1.00 da file ASCII
      -----------------------------------------------------------------

   Questo modulo permette la lettura di un file ASCII contenente la tabella
   di transizione nel seguente formato

   - prima riga: max numero di colori
   - righe successive: considerando N colori le prime N righe conterranno
     i dati del primo stato, il secondo gruppo di N righe il secondo
     stato e cosç via.  All'interno del gruppo ogni riga rappresenta il
     colore relativo alla sua posizione e deve essere impostata con il
     formato: nextstatus,nextcolor,direzione
     per quest'ultima ä controllata la sola iniziale (in italiano)

   Il file deve avere estensione .TAB
 --------------------------------------------------------------------------- *)

procedure gettabella;

  procedure findvalue(var s : string; var x : integer);
  (*
     lettura dati numerici dalla stringa
  *)
  var j,k : integer;
  begin
  j := pos(',',s);
  val(copy(s,1,j-1),x,k);
  delete(s,1,j);
  end;

var data : text;
    d : string;
    j,s,maxc : integer;
begin
(*
   apertura del file, l'estensione .TAB ä aggiunta automaticamente
*)
assign(data,paramstr(1)+'.tab');
reset(data);
readln(data,maxc);
s := 1;

repeat
  for j := 1 to maxc do with tabella[s,j-1] do begin
  readln(data,d);
  findvalue(d,nextstatus);
  findvalue(d,nextcolor);
  case d[1] of
    'a' : dir := avanti;
    'i' : dir := indietro;
    's' : dir := sinistra;
    'd' : dir := destra;
    end;
  end;
  inc(s);
(*
   non sapendo a priori quanti stati contiene la tabella dobbiamo
   verificare il termine del file
*)
  until eof(data);
close(data);
xc := nx div 2;
yc := ny div 2;
end;
