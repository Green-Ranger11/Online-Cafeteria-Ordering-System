import { Component, OnInit, Input } from '@angular/core';
import { IMeal } from 'src/app/shared/models/meal';
import { AdminService } from '../admin.service';
import { ToastrService, Toast } from 'ngx-toastr';
import { HttpEventType, HttpEvent } from '@angular/common/http';

@Component({
  selector: 'app-edit-meal-photos',
  templateUrl: './edit-meal-photos.component.html',
  styleUrls: ['./edit-meal-photos.component.scss'],
})
export class EditMealPhotosComponent implements OnInit {
  @Input() meal: IMeal;
  progress = 0;
  addPhotoMode = false;

  constructor(
    private adminService: AdminService,
    private toast: ToastrService
  ) {}

  ngOnInit(): void {}

  uploadFile(file: File) {
    this.adminService.uploadImage(file, this.meal.id).subscribe(
      (event: HttpEvent<any>) => {
        switch (event.type) {
          case HttpEventType.UploadProgress:
            this.progress = Math.round((event.loaded / event.total) * 100);
            break;
          case HttpEventType.Response:
            this.meal = event.body;
            setTimeout(() => {
              this.progress = 0;
              this.addPhotoMode = false;
            }, 1500);
        }
      },
      (error) => {
        if (error.errors) {
          this.toast.error(error.errors[0]);
        } else {
          this.toast.error('Problem uploading image');
        }
        this.progress = 0;
      }
    );
  }

  deletePhoto(photoId: number) {
    this.adminService.deleteMealPhoto(photoId, this.meal.id).subscribe(
      () => {
        const photoIndex = this.meal.photos.findIndex((x) => x.id === photoId);
        this.meal.photos.splice(photoIndex, 1);
      },
      (error) => {
        this.toast.error('Problem deleting photo');
        console.log(error);
      }
    );
  }

  setMainPhoto(photoId: number) {
    this.adminService
      .setMainPhoto(photoId, this.meal.id)
      .subscribe((meal: IMeal) => {
        this.meal = meal;
      });
  }

  addPhotoModeToggle() {
    if (this.addPhotoMode) {
      this.addPhotoMode = false;
    } else {
      this.addPhotoMode = true;
    }
  }
}
