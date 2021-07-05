import {
  Component,
  ElementRef,
  OnInit,
  ViewChild,
  OnChanges,
  AfterContentChecked,
  AfterViewChecked,
  AfterContentInit,
  ChangeDetectorRef, AfterViewInit
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
import {ActivatedRoute} from "@angular/router";

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
export class PotionListComponent implements OnInit, AfterViewChecked {

  public potions: Array<Potion> = new Array<Potion>();
  public columnList: Array<string> = ['name','spellName','levell','price',];

  @ViewChild("idTable") table!: MatTable<any>;
  @ViewChild("card",{read:ElementRef}) card!:ElementRef;
  @ViewChild("card2",{read:ElementRef}) card2!:ElementRef;
  @ViewChild("editorForm") editForm!:PotionEditorComponent;
  public pos: Position = {left:1000,top:1000};
  public show: boolean = false;
  expandedElement: Potion | null = null;
  public newPotion: Potion = new Potion(0);


  constructor(public fetcData:FetchDataService, public change: ChangeDetectorRef, private activatedRoute: ActivatedRoute) {

  }

  ngOnInit(): void {
    this.fetcData.getAllPotion().subscribe((result:Array<any>)=>{
      console.log(result)
      if(result.length != this.potions.length) {
        result.forEach(value => {
          this.potions.push(new Potion(
            value['id'],
            value['name'],
            value['description'],
            value['source'],
            new Price(
              value['price']['value'],
              value['price']['coin']),
            new SpellName(
              value['spellName']['id'],
              value['spellName']['name']),
            value['levell']))
        })

      }
      this.table.renderRows();
    });

  }

  ngAfterViewChecked() {


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

  extendelement(element:Potion){
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

  this.fetcData.deletePotion(this.expandedElement!.id).subscribe(()=>{

    this.potions.splice(this.potions.indexOf(this.expandedElement!,0),1);
    this.expandedElement = null;
    this.table.renderRows();

  })
  }





}
