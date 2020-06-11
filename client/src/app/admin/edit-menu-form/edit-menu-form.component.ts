import { Component, OnInit, Input } from '@angular/core';
import { MenuFormValues } from 'src/app/shared/models/menu';
import { IRestaurant } from 'src/app/shared/models/restaurants';
import { ActivatedRoute, Router } from '@angular/router';
import { AdminService } from '../admin.service';

@Component({
  selector: 'app-edit-menu-form',
  templateUrl: './edit-menu-form.component.html',
  styleUrls: ['./edit-menu-form.component.scss']
})
export class EditMenuFormComponent implements OnInit {
  @Input() menu: MenuFormValues;
  @Input() restaurants: IRestaurant[];

  constructor(private route: ActivatedRoute, private adminService: AdminService, private router: Router) { }

  ngOnInit(): void {
  }

  onSubmit(menu: MenuFormValues) {
    if (this.route.snapshot.url[0].path === 'editMenu') {
      const updateMenu: MenuFormValues = { ...this.menu, ...menu};
      this.adminService
        .updateMenu(updateMenu, +this.route.snapshot.paramMap.get('id'))
        .subscribe((response: any) => {
          this.router.navigate(['/admin']);
        });
    } else {
      const newMenu = { ...menu};
      this.adminService.createMenu(newMenu).subscribe((response: any) => {
        this.router.navigate(['/admin']);
      });
    }
  }

}
