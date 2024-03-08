export class GridTab {
  title: string;
  filter: { field: string, criteria: (value: any) => boolean };
}