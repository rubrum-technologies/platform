// eslint-disable-next-line @typescript-eslint/ban-ts-comment
// @ts-expect-error
import WinBox from 'winbox/src/js/winbox';

export interface WindowOptions {
  top?: number;
  right?: number;
  bottom?: number;
  left?: number;
  background?: string;
}

export class Window {
  private readonly win: WinBox;

  public constructor(
    public readonly title: string,
    root: Element | null,
    options: WindowOptions,
  ) {
    this.win = new WinBox(title, {
      ...options,
      root,
    });
  }

  public get root(): HTMLDivElement {
    return this.win.dom;
  }

  public get body(): HTMLDivElement {
    return this.win.body;
  }
}
