import { ComponentFixture, TestBed } from '@angular/core/testing';
import { MenuAppsComponent } from './menu-apps.component';

describe('MenuAppsComponent', () => {
  let component: MenuAppsComponent;
  let fixture: ComponentFixture<MenuAppsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MenuAppsComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(MenuAppsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
