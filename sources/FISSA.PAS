(* --------------------------------------------------------------------------
              Modulo a programma fisso per TURMITE 1.00
              -----------------------------------------

    Con questo modulo la tabella di transizione ha sempre lo stesso schema:
    il punto di colore N viene colorato di (N+1) mod COLORS e si gira a
    destra se N Š pari, a sinistra se N Š dispari

    Da linea comando va specificato COLORS ed eventualmente le coordinate
    iniziali (per default a centro schermo)
---------------------------------------------------------------------------- *)

procedure gettabella;
var j,v : integer;

  procedure qwe(d : string);
  (*
     messaggi di errore e fine programma
  *)
  begin
  restorecrtmode;
  writeln('Errore: ',d,' fuori range ammissibile.');
  halt(1);
  end;

begin
(*
   riempimento tabella di transizione
*)
for j := 0 to 15 do
with tabella[1,j] do begin
  nextstatus := 1; nextcolor := j+1; dir := destra;
  if (j mod 2) = 1 then dir := sinistra;
  end;

(*
   verifica numero parametri
*)
if (paramcount < 1) or (paramcount = 2) or (paramcount > 3) then begin
  restorecrtmode;
  writeln('Uso: TURMITE colors [xc yc]');
  halt(2);
  end;
val(paramstr(1),v,j);
if (v < 2) or (v > 16) then qwe('colori');

(*
   tronca la tabella all' N-esimo colore
*)
tabella[1,v-1].nextcolor := 0;

(*
   assegna le coordinate iniziali
*)
if paramcount = 3 then begin
  val(paramstr(2),xc,j);
  if (xc < 0) or (xc > nx) then qwe('coordinata X');
  val(paramstr(3),yc,j);
  if (yc < 0) or (yc > ny) then qwe('coordinata Y');
  end
else begin
  xc := nx div 2;  yc := ny div 2;
  end;
end;
