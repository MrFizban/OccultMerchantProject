import {
  Component,
  ElementRef,
  OnInit,
  ViewChild,
  OnChanges,
  AfterContentChecked,
  AfterViewChecked,
  ChangeDetectorRef
} from '@angular/core';
import {FetchDataService} from "../fetch-data.service";
import {Potion, SpellName} from "../Items/Potion";
import {CoinType, Price} from "../Items/Base";
import {MatTable} from "@angular/material/table";
import {MatCard} from "@angular/material/card";
import {animate, state, style, transition, trigger} from "@angular/animations";
import {isExtended} from "@angular/compiler-cli/src/ngtsc/shims/src/expando";
import {PotionEditorComponent} from "./potion-editor/potion-editor.component";
import {Position} from "../Items/Position";

@Component({
  selector: 'app-potion-list',
  templateUrl: './potion-list.component.html',
  styleUrls: ['./potion-list.component.css'],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({height: '0px', minHeight: '0'})),
      state('expanded', style({height: '*'})),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
    ]
})
export class PotionListComponent implements OnInit,OnChanges,AfterContentChecked, AfterViewChecked {

  public potions: Array<Potion> = new Array<Potion>();
  public columnList: Array<string> = ['name','spellName','price'];

  @ViewChild("idTable") table!: MatTable<any>;
  @ViewChild("card",{read:ElementRef}) card!:ElementRef;
  @ViewChild("card2",{read:ElementRef}) card2!:ElementRef;
  @ViewChild("editorForm") editForm!:PotionEditorComponent;
  public pos: Position = {left:1000,top:1000};
  public show: boolean = false;
  expandedElement: Potion | null = null;
  addNewVisible: boolean = true;
  public newPotion: Potion = new Potion(0,"gianni");


  constructor(private fetcData:FetchDataService, private change: ChangeDetectorRef) {
    this.pos.left
  }

  ngOnInit(): void {
    this.fetcData.getAllPotion().subscribe((result:Array<any>)=>{
      result.forEach(value => {
        console.log("value:\t" );
        console.log(value);
        console.log("spell:\t" + value['spellName']['name']);
        console.log(value['spellName']['name'])
        this.potions.push(new Potion(value['id'],value['name'],value['description'],value['source'],
          new Price(value['price']['value'], value['price']['coin']),new SpellName(value['spellName']['id'],value['spellName']['name']),value['levell']))
      })
      this.table.renderRows();
      console.log(this.potions)
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

  extendelement(element:Potion){
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
      this.newPotion = new Potion();
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
  this.fetcData.deletePotion(this.expandedElement!.id).subscribe(()=>{
    console.log(this.potions.length)
    this.potions.splice(this.potions.indexOf(this.expandedElement!,0));
    this.expandedElement = null;
    this.table.renderRows();
    console.log(this.potions.length)
  })
  }


  saveNewPotionButton(){
    this.editForm.addToDatabase().subscribe(()=>{
      this.potions.push(this.editForm.potion);
      this.table.renderRows();
    })
  }
}
