import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { MenuService } from '../../services/menu.service';
import { MenuItem } from '../../models/menu-item';

@Component({
  selector: 'app-menu-list',
  templateUrl: './menu-list.component.html',
  styleUrls: ['./menu-list.component.scss']
})
export class MenuListComponent implements OnInit {
  menuItems: MenuItem[] = [];
  @Output() menuItemSelected: any = new EventEmitter<any>();
  @Output() updateSelected: any = new EventEmitter<any>();
  isLoading: boolean = false;

  constructor(private menuService: MenuService) { }

  ngOnInit(): void {
    this.loadMenu();
  }

  loadMenu(): void {
    this.menuService.getMenuItems().subscribe((data) => {
      this.menuItems = data;
    });
  }

  deleteItem(id: number): void {
    this.isLoading = true;
    this.menuService.deleteMenuItem(id).subscribe(() => {
      this.menuItems = this.menuItems.filter(item => item.id !== id);
      this.isLoading = false;
    });
  }

  onEditClick(item: any) {
    this.isLoading = true;
    this.menuService.getMenuItemById(item).subscribe((data) => {
      this.menuItemSelected.emit(data);
      this.updateSelected.emit('update');
      this.isLoading = false;
    })
  }
}
