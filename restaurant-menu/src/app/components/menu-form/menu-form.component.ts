import { Component, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MenuService } from '../../services/menu.service';
import { Router } from '@angular/router';
import { MenuListComponent } from '../menu-list/menu-list.component';

@Component({
  selector: 'app-menu-form',
  templateUrl: './menu-form.component.html',
  styleUrls: ['./menu-form.component.scss']
})
export class MenuFormComponent {
  menuForm: FormGroup;
  @ViewChild(MenuListComponent) menuListComponent!: MenuListComponent;
  selectedItem: any;
  updatedItem: any;
  isLoading: boolean = false;

  constructor(
    private fb: FormBuilder,
    private menuService: MenuService,
    private router: Router
  ) {
    this.menuForm = this.fb.group({
      id: [null],
      name: ['', Validators.required],
      description: [''],
      price: ['', [Validators.required, Validators.min(1)]],
      category: ['', Validators.required],
    });
  }

  // Getter Methods for Validation
  get name() {
    return this.menuForm.get('name');
  }

  get price() {
    return this.menuForm.get('price');
  }

  get description() {
    return this.menuForm.get('description');
  }

  get category() {
    return this.menuForm.get('category');
  }

  submitForm(): void {
    if (this.menuForm.invalid) {
      this.menuForm.markAllAsTouched(); // Mark all fields as touched to show errors
      return;
    }

    this.isLoading = true;
    this.menuService.addOrUpdate(this.menuForm.value).subscribe(() => {
      this.menuListComponent.loadMenu();
      this.isLoading = false;
      this.menuForm.reset();
      this.updatedItem = 'save';
    });
  }

  handleMenuItemSelected(menuItem: any) {
    this.selectedItem = menuItem;
    this.menuForm.patchValue(this.selectedItem);
  }

  updateItemSelected(updatedItem: any) {
    this.updatedItem = updatedItem;
  }
}
