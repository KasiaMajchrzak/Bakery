import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css'],
  animations: []
})
export class NavMenuComponent {
  isExpanded = false;
  menuItems: any[] = [
   { title: 'Strona główna', icon: 'home', path: '' },
   { title: 'Torty', icon: 'keyboard_arrow_right', expanded: false, path: 'birthday-cakes', children: [
    { title: 'Kreator', icon: 'create', path: 'birthday-cake' },
    { title: 'Gotowe propozycje', icon: 'home', path: 'birthday-cakes' }
   ]},
   { title: 'Mono-desery', icon: 'keyboard_arrow_right', expanded: false, path: 'mono-desserts', children: [
    { title: 'Kreator', icon: 'create', path: 'mono-dessert' },
    { title: 'Gotowe propozycje', icon: 'home', path: 'mono-desserts' }
   ]},
   { title: 'Statystyki', icon: 'query_stats', path: '' }
  ];

 
  constructor(
    private route: ActivatedRoute,
    private router: Router
  ) { }

  navigate(item: any) {
    console.log('navigate', item);
    if(item.children && item.children.length > 0) {
      item.expanded = !item.expanded;
    } else {
      this.router.navigate([item.path]);
    }
  }
}
