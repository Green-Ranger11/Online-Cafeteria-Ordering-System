import { Component, OnInit, Input } from '@angular/core';
import { IMeal, Ingrediant, IngrediantsFormValues } from 'src/app/shared/models/meal';
import { ActivatedRoute, Router } from '@angular/router';
import { AdminService } from '../admin.service';

@Component({
  selector: 'app-edit-ingrediants',
  templateUrl: './edit-ingrediants.component.html',
  styleUrls: ['./edit-ingrediants.component.scss'],
})
export class EditIngrediantsComponent implements OnInit {
  @Input() thisMeal: IMeal;
  defaultMeal = this.thisMeal;
  editIngrediantMode = null;
  newIngrediantMode = false;
  newIngrediant: IngrediantsFormValues = new IngrediantsFormValues();

  constructor(
    private route: ActivatedRoute,
    private adminService: AdminService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.newIngrediant = new IngrediantsFormValues();
  }

  editIngrediantModeToggle(id: number, index: number) {
    if (this.editIngrediantMode) {
      this.editIngrediantMode = null;
      this.thisMeal.ingrediants[index] = this.defaultMeal.ingrediants[index];
    } else {
      this.editIngrediantMode = id;
    }
  }

  newIngrediantModeToggle() {
    if (this.newIngrediantMode) {
      this.newIngrediantMode = null;
    } else {
      this.newIngrediantMode = true;
    }
  }

  onSubmit(ingrediant: Ingrediant) {
    this.adminService
      .updateMealIngrediant(
        ingrediant,
        +this.route.snapshot.paramMap.get('id'),
        ingrediant.id
      )
      .subscribe((response: any) => {
        this.router.navigate(['/admin/edit/' + this.thisMeal.id]);
        this.editIngrediantMode = null;
      });
  }

  onNewSubmit(ingrediant: IngrediantsFormValues) {
    this.adminService
      .addMealIngrediant(
        ingrediant,
        +this.route.snapshot.paramMap.get('id'),
      )
      .subscribe((response: any) => {
        this.router.navigate(['/admin/edit/' + this.thisMeal.id]);
        this.thisMeal = response;
        this.newIngrediantMode = null;
      });
  }

  deleteIngrediant(id: number){
    this.adminService
      .deleteMealIngrediant(
        id,
        +this.route.snapshot.paramMap.get('id'),
      )
      .subscribe((response: any) => {
        this.thisMeal.ingrediants.splice(this.thisMeal.ingrediants.findIndex(p => p.id === id), 1);
        this.router.navigate(['/admin/edit/' + this.thisMeal.id]);
      });
  }


  updatePrice(event: any, i: number) {
    this.thisMeal.ingrediants[i].price = event;
  }

  updateIngrediantPrice(event: any) {
    this.newIngrediant.price = event;
  }
}
