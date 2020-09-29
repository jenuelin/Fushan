import { Component, OnInit, OnChanges, Input, Output, EventEmitter, SimpleChanges } from '@angular/core';
import paginate from 'jw-paginate';

@Component({
  selector: 'app-pagination',
  templateUrl: './pagination.component.html',
  styleUrls: ['./pagination.component.less']
})
export class PaginationComponent implements OnInit, OnChanges {

  @Input() items: Array<any>;
  @Output() changePage = new EventEmitter<any>(true);
  @Input() initialPage = 1;
  @Input() pageSize = 10;
  @Input() maxPages = 10;
  @Input() totalCount = 0;
  @Input() page = 1;

  pager: any = {};

  ngOnInit() {
  }

  ngOnChanges(changes: SimpleChanges) {
    // reset page if items array has changed
    if (changes.items.currentValue !== changes.items.previousValue) {
      this.setPage(this.page);
    }
  }

  emitPage(page: number) {
    this.changePage.emit(page);
  }

  setPage(page: number) {
    this.pager = paginate(this.totalCount, page, this.pageSize, this.maxPages);
  }

  displayPageBtn(page: number) {
    return page === 1 || (page <= 5 && this.pager.currentPage <= 5) ||
      this.pager.currentPage === page ||
      page === this.pager.totalPages ||
      (this.pager.currentPage + 3 > page && this.pager.currentPage - 3 < page) ||
      (page > this.pager.totalPages - 5 && this.pager.currentPage > this.pager.totalPages - 5);
  }
  displayBeforeThreePoint(page: number) {
    return this.pager.totalPages > 7 &&
      this.pager.currentPage > 5 &&
      (this.pager.currentPage - 3 > 1 &&
      ((this.pager.currentPage - 3 === page && this.pager.totalPages - 5 > page) || (this.pager.totalPages - 5 === page && this.pager.currentPage > this.pager.totalPages - 3)));
  }
  displayAfterThreePoint(page: number) {
    if (this.pager.totalPages - 5 < this.pager.currentPage) return false;
    if (this.pager.totalPages <= 5) return false;
    return (this.pager.currentPage < 4 && page === 6) ||
      (this.pager.currentPage >= 4 && this.pager.totalPages > this.pager.currentPage + 3 && this.pager.currentPage + 3 === page);
  }
}
