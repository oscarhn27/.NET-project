import { Component, OnInit} from '@angular/core';
import { ServiceSettings } from '../service-settings';

@Component({
  selector: 'home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit{

  constructor(
    private _mServ: ServiceSettings
  ) { }

  async ngOnInit(): Promise<any> {
    try {
      await this._mServ.load()
    } catch (e) {
      console.log(e)
    }
    
  }

}
