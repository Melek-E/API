import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DevExtremeTestComponent } from './dev-extreme-test.component';

describe('DevExtremeTestComponent', () => {
  let component: DevExtremeTestComponent;
  let fixture: ComponentFixture<DevExtremeTestComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DevExtremeTestComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DevExtremeTestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
