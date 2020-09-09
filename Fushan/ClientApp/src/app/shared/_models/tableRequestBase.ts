export class TableRequestBase {
  page: number = 1;
  rows: number = 10;
  orderBy: string = "ASC";
  sortBy: string = "createdOn";
  showAll: boolean = false;
}
