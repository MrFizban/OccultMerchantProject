import {ChangeDetectorRef, Component, ElementRef, OnInit, ViewChild} from '@angular/core';
import {CasterPossibility, Spell} from "../Items/Spell";
import {MatTable} from "@angular/material/table";
import {FetchDataService} from "../fetch-data.service";
import {Price} from "../Items/Base";
import {Position} from "../Items/Position";
import {SpellEditorComponent} from "./spell-editor/spell-editor.component";

@Component({
  selector: 'app-spell-list',
  templateUrl: './spell-list.component.html',
  styleUrls: ['./spell-list.component.css']
})
export class SpellListComponent implements OnInit {

  public spells: Array<Spell> = new Array<Spell>();
  public columnList: Array<string> = ['name','levell','price'];

  @ViewChild("idTable") table!: MatTable<any>;
  @ViewChild("card",{read:ElementRef}) card!:ElementRef;
  @ViewChild("card2",{read:ElementRef}) card2!:ElementRef;
  @ViewChild("editorForm") editForm!:SpellEditorComponent;
  public pos: Position = {left:1000,top:1000};
  public show: boolean = false;
  expandedElement: Spell | null = null;
  public newSpell: Spell = new Spell(0);


  constructor(private fetcData:FetchDataService, public change: ChangeDetectorRef) {
    this.pos.left
  }

  ngOnInit(): void {
    this.fetcData.getAllSpell().subscribe((result:Array<any>)=>{
      result.forEach(value => {
        this.spells.push(new Spell(value['id'],value['name'],value['description'],value['source'],
          new Price(value['price']['value'], value['price']['coin']),value['casterPossibility'],value['levell']))
      })
      this.table.renderRows();
      console.log(this.spells)
    });
  }

  swicthshow(){
    if(this.expandedElement != null) {
      this.change.detectChanges();
      this.pos.left -= this.card2.nativeElement.offsetLeft;
      this.pos.top -= this.card2.nativeElement.offsetTop;
      this.change.detectChanges();
      this.pos.left += this.card.nativeElement.offsetLeft - 100;
      this.pos.top += this.card.nativeElement.offsetTop;
      this.change.detectChanges();
      console.log("p:\t" + this.card.nativeElement.offsetTop + "\ts:\t" + this.card2.nativeElement.offsetTop);

    }
  }

  extendelement(element:Spell){
    if(!this.editForm.editFormVisible) {
      if (this.expandedElement != element) {
        this.expandedElement = element;

        this.swicthshow();
      } else if (this.expandedElement === element) {
        this.expandedElement = null;
      }
    }
  }




  delete (){
    console.log("id:\t" + this.expandedElement!.id);
    console.log(this.expandedElement!);
    this.fetcData.deleteSpell(this.expandedElement!.id).subscribe(()=>{
      console.log(this.spells.length)
      this.spells.splice(this.spells.indexOf(this.expandedElement!,0),1);
      this.expandedElement = null;
      this.table.renderRows();
      console.log(this.spells.length)
    })
  }



}
