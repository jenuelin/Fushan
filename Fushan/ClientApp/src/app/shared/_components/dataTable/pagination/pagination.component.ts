import { Component, OnInit, OnChanges, Input, Output, EventEmitter, SimpleChanges } from '@angular/core';
import paginate from 'jw-paginate';
import { PagingService } from '@shared/_services/paging.service';

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

  pager: any = {};

  constructor(private _pagingService: PagingService) {
    //_pagingService.navItem$.subscribe(page => {
    //  this.pager = paginate(this.totalCount, page, this.pageSize, this.maxPages);
    //})
  }
  ngOnInit() {
    // set page if items array isn't empty
    //if (this.items && this.items.length) {
    //  this.setPage(this.initialPage);
    //}
    this._pagingService.navItem$.subscribe(page => {
      this.pager = paginate(this.totalCount, page, this.pageSize, this.maxPages);
    })
  }

  ngOnChanges(changes: SimpleChanges) {
    // reset page if items array has changed
    //if (changes.items.currentValue !== changes.items.previousValue) {
    //  this.setPage(this.initialPage);
    //}
  }

  emitPage(page: number) {
    //this._pagingService.changeNav(page);
    //this._pagingService.navItem$.subscribe(data => {
    //  this.pager = paginate(this.totalCount, page, this.pageSize, this.maxPages);
    //});
    //this.changePage.emit(page).subscribe(data => {
    //  this.pager = paginate(this.totalCount, page, this.pageSize, this.maxPages);
    //});
    // get new pager object for specified page
    //this.pager = paginate(this.items.length, page, this.pageSize, this.maxPages);

    // get new page of items from items array
    //var pageOfItems = this.items.slice(this.pager.startIndex, this.pager.endIndex + 1);
    //var pageOfItems = this.items;

    // call change page function in parent component
    this.changePage.emit(page);
  }

  setPage(page: number) {
    this.pager = paginate(this.totalCount, page, this.pageSize, this.maxPages);
  }
}
