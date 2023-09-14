import { Component, OnInit, OnChanges, SimpleChanges, Input, Output, EventEmitter } from '@angular/core';
import Player from '@vimeo/player';

@Component({
  selector: 'app-vimeo-aula-online',
  templateUrl: './vimeo-aula-online.component.html',
  styleUrls: ['./vimeo-aula-online.component.scss']
})
export class VimeoAulaOnlineComponent implements OnInit, OnChanges {
  @Input() vimeoUrl: string;
  @Input() vimeoUrl1: string;
  @Input() vimeoProgress: number;
  @Output() onAssistido: EventEmitter<any> = new EventEmitter();
  options: any;
  player: any;
  options1: any;
  player1: any;
  assistiu: boolean = false

  ngOnChanges(changes: SimpleChanges): void {
    if (!changes?.vimeoUrl) return;
    const { currentValue } = changes.vimeoUrl;
    if(currentValue) {
      this.vimeoUrl = currentValue;
      this.triggerVideo();
    }
  }

  ngOnInit(): void {
    this.triggerVideo();

  }

  triggerVideo(): void {

    this.options = {
      id: this.vimeoUrl,
      responsive: true,
      loop: false
    };

    this.player = new Player('vimeo-video', this.options);

    if(this.vimeoProgress) this.player.setCurrentTime(this.vimeoProgress);

    this.player.on('progress', res => {
      if(!this.assistiu && res?.percent > 0.9) {
        this.assistiu = true;

        const data = {
          vimeoUrl: this.vimeoUrl,
          tempo: res?.seconds
        };
        this.onAssistido.emit(data);
      }
    });

    this.player.on('pause', res => {
      const data = {
        vimeoUrl: this.vimeoUrl,
        tempo: res?.seconds
      };
      this.onAssistido.emit(data);
    });

    this.player.on('play', res => {
      if(!this.vimeoProgress) {
        const data = {
          vimeoUrl: this.vimeoUrl,
          tempo: 0
        };
        this.onAssistido.emit(data);
      }
    });
  }


  triggerVideo1(): void {

    this.options1 = {
      id: this.vimeoUrl1,
      responsive: true,
      loop: false
    };

    this.player1 = new Player('vimeo-video1', this.options1);

    // if(this.vimeoProgress) this.player1.setCurrentTime(this.vimeoProgress);

    // this.player1.on('progress', res => {
    //   if(!this.assistiu && res?.percent > 0.9) {
    //     this.assistiu = true;

    //     const data = {
    //       vimeoUrl1: this.vimeoUrl1,
    //       tempo: res?.seconds
    //     };
    //     this.onAssistido.emit(data);
    //   }
    // });

    // this.player1.on('pause', res => {
    //   const data = {
    //     vimeoUrl1: this.vimeoUrl1,
    //     tempo: res?.seconds
    //   };
    //   this.onAssistido.emit(data);
    // });

    // this.player1.on('play', res => {
    //   if(!this.vimeoProgress) {
    //     const data = {
    //       vimeoUrl1: this.vimeoUrl1,
    //       tempo: 0
    //     };
    //     this.onAssistido.emit(data);
    //   }
    // });
  }

}
