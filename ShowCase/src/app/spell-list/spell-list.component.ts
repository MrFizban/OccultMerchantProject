import {ChangeDetectorRef, Component, ElementRef, OnInit, ViewChild} from '@angular/core';
import {CasterPossibility, Spell} from "../Items/Spell";
import {MatTable} from "@angular/material/table";
import {FetchDataService} from "../fetch-data.service";
import {Price} from "../Items/Base";
import {Position} from "../Items/Position";
import {SpellEditorComponent} from "./spell-editor/spell-editor.component";
import {ActivatedRoute, Router} from "@angular/router";
import {createLogErrorHandler} from "@angular/compiler-cli/ngcc/src/execution/tasks/completion";
import {Potion} from "../Items/Potion";

@Component({
  selector: 'app-spell-list',
  templateUrl: './spell-list.component.html',
  styleUrls: ['./spell-list.component.css']
})
export class SpellListComponent implements OnInit {

  public spells: Array<Spell> = new Array<Spell>();
  public columnList: Array<string> = ['name','levell','price'];
  // utilizzato quando la panina viene chiamate per collegare pozione e incantesimo
  public editingPotion : number = -1;
  @ViewChild("idTable") table!: MatTable<any>;
  @ViewChild("card",{read:ElementRef}) card!:ElementRef;
  @ViewChild("card2",{read:ElementRef}) card2!:ElementRef;
  @ViewChild("editorForm") editForm!:SpellEditorComponent;
  public pos: Position = {left:1000,top:1000};
  public show: boolean = false;
  expandedElement: Spell | null = null;
  public newSpell: Spell = new Spell(0);


  constructor(private fetcData:FetchDataService, public change: ChangeDetectorRef, public activatedRoute:ActivatedRoute, public router:Router) {
    this.pos.left
  }

  ngOnInit(): void {
    this.fetcData.getAllSpell().subscribe((result:Array<any>)=>{
      result.forEach(value => {
        this.spells.push(new Spell(value['id'],value['name'],value['description'],value['source'],
          new Price(value['price']['value'], value['price']['coin']),value['casterPossibility'],value['levell']))
      })
      this.table.renderRows();


      this.activatedRoute.paramMap.subscribe((param) =>{
         if(param.get('id')){
           this.editingPotion = parseInt(param.get('id')!);
         }
      });


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

    }
  }

  extendelement(element:Spell){
    if(this.editingPotion == -1) {
      if (!this.editForm.editFormVisible) {
        if (this.expandedElement != element) {
          this.expandedElement = element;
          this.swicthshow();
        } else if (this.expandedElement === element) {
          this.expandedElement = null;
        }
      }
    }else{
      this.expandedElement = element;
    }
  }




  delete (){

    this.fetcData.deleteSpell(this.expandedElement!.id).subscribe(()=>{
      this.spells.splice(this.spells.indexOf(this.expandedElement!,0),1);
      this.expandedElement = null;
      this.table.renderRows();
    })
  }


  savePotionSpell() {
    if(this.expandedElement){
      let tmpPotion : Potion = new Potion(this.editingPotion);
      tmpPotion.spellName.id = this.expandedElement.id;
      tmpPotion.spellName.name = this.expandedElement.name;
      tmpPotion.filter.names=["spell"];
      console.log("this potion:")
      this.fetcData.updatePotion(tmpPotion).subscribe();
    }
    this.router.navigate(['/potion',{newPotionSpell:true}]);
  }
}
