import {ChangeDetectorRef, Component, ElementRef, OnInit, ViewChild} from '@angular/core';
import {CasterPossibility, Spell} from "../Items/Spell";
import {Potion, SpellName} from "../Items/Potion";
import {MatTable} from "@angular/material/table";
import {PotionEditorComponent} from "../potion-list/potion-editor/potion-editor.component";
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
  public columnList: Array<string> = ['name','spellName','price'];

  @ViewChild("idTable") table!: MatTable<any>;
  @ViewChild("card",{read:ElementRef}) card!:ElementRef;
  @ViewChild("card2",{read:ElementRef}) card2!:ElementRef;
  @ViewChild("editorForm") editForm!:SpellEditorComponent;
  public pos: Position = {left:1000,top:1000};
  public show: boolean = false;
  expandedElement: Spell | null = null;
  addNewVisible: boolean = true;
  public newSpell: Spell = new Spell(0,"gianni");


  constructor(private fetcData:FetchDataService, private change: ChangeDetectorRef) {
    this.pos.left
  }

  ngOnInit(): void {
    this.fetcData.getAllSpell().subscribe((result:Array<any>)=>{
      result.forEach(value => {
        console.log("value:\t" );
        console.log(value);
        console.log("spell:\t" + value['spellName']['name']);
        console.log(value['spellName']['name'])
        this.spells.push(new Spell(value['id'],value['name'],value['description'],value['source'],
          new Price(value['price']['value'], value['price']['coin']),new Array<CasterPossibility>(),value['level']));
      })
      this.table.renderRows();
      console.log(this.spells)
    });
  }
  ngOnChanges(){
    // this.swicthshow();
  }

  ngAfterContentChecked() {
  }

  ngAfterViewChecked() {
    //this.swicthshow();
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
    if(!this.addNewVisible) {
      if (this.expandedElement != element) {
        this.expandedElement = element;

        this.swicthshow();
      } else if (this.expandedElement === element) {
        this.expandedElement = null;
      }
    }
  }

  addNew(){
    if(!this.addNewVisible) {
      this.newSpell = new Spell();
      this.expandedElement = null;
      this.addNewVisible = true;
    }else{
      this.addNewVisible = false;
      this.change.detectChanges();
      this.addNew();
    }
  }



  delete (){
    console.log("id:\t" + this.expandedElement!.id);
    console.log(this.expandedElement!);
    this.fetcData.deleteSpell(this.expandedElement!.id).subscribe(()=>{
      console.log(this.spells.length)
      this.spells.splice(this.spells.indexOf(this.expandedElement!,0));
      this.expandedElement = null;
      this.table.renderRows();
      console.log(this.spells.length)
    })
  }


  saveNewSpellButton(){
    this.editForm.addToDatabase().subscribe(()=>{
      this.spells.push(this.editForm.spell);
      this.table.renderRows();
    })
  }
}
