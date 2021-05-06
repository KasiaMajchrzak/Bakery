import { ElementRef, OnInit, ViewChild } from '@angular/core';
import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

declare const $: any;

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css'],
  animations: []
})
export class NavMenuComponent implements OnInit {
  isExpanded = false;
  expanded = false;
  ps: any;
  menuItems: any[] = [
   { title: 'Strona główna', icon: 'home', path: '' },
   { title: 'Torty', icon: 'keyboard_arrow_right', expanded: false, path: 'birthday-cakes', children: [
    { title: 'Kreator', icon: 'create', path: '/birthday-cakes/birthday-cake' },
    { title: 'Gotowe propozycje', icon: 'home', path: '/birthday-cakes/birthday-cakes' }
   ]},
   { title: 'Mono-desery', icon: 'keyboard_arrow_right', expanded: false, path: 'mono-desserts', children: [
    { title: 'Kreator', icon: 'create', path: '/mono-desserts/mono-dessert' },
    { title: 'Gotowe propozycje', icon: 'home', path: '/mono-desserts/mono-desserts' }
   ]},
   { title: 'Statystyki', icon: 'query_stats', path: '' }
  ];

  @ViewChild('div', { static: false }) div: ElementRef;
 
  constructor(
    private route: ActivatedRoute,
    private router: Router
  ) { }

  ngOnInit() {
    if(window.matchMedia(`(min-width: 960px)`).matches && !this.isMac()) {
      this.ps.update();
    }
  }

  navigate(path: string) {
    this.router.navigateByUrl(path);
  }

  // navigate(item: any) {
  //   console.log('navigate', item);
  //   if(item.children && item.children.length > 0) {
  //     item.expanded = !item.expanded;
  //   } else {
  //     this.router.navigate([item.path]);
  //   }
  // }

  async collapse() {
    this.expanded = !this.expanded;
    if (this.expanded) {
      this.div.nativeElement.classList.add('collapsing');
      this.div.nativeElement.classList.add('show');
      this.div.nativeElement.style.height = `${this.div.nativeElement.firstChild.clientHeight + 15}px`;

      await new Promise(resolve => setTimeout(resolve, 350)); // wait for transition end
      this.div.nativeElement.classList.remove('collapsing');
      this.div.nativeElement.style.height = '';
    } else {
      this.div.nativeElement.style.height = `${this.div.nativeElement.firstChild.clientHeight + 15}px`;
      this.div.nativeElement.className = 'collapsing';
      await new Promise(resolve => setTimeout(resolve, 1));
      this.div.nativeElement.style.height = '';

      await new Promise(resolve => setTimeout(resolve, 350)); // wait for transition end
      this.div.nativeElement.classList.remove('collapsing');
      this.div.nativeElement.classList.add('collapse');
    }
  }

  updatePS(): void{
    if(window.matchMedia(`(min-width: 960px)`).matches && !this.isMac()) {
      this.ps.update();
    }
  }

  isMac(): boolean {
    let bool = false;
    if (navigator.platform.toUpperCase().indexOf('MAC') >= 0 || navigator.platform.toUpperCase().indexOf('IPAD') >= 0) {
      bool = true;
    }

    return true;
  }
}
