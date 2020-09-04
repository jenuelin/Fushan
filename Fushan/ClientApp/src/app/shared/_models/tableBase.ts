export interface TableBase {
}

export class TableList<T> {
  valid: boolean;
  count: number;
  pagePageIndex: number;
  totalPages: number;
  table: T;
}
