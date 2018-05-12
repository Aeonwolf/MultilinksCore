import { Component, Input, OnInit } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http/src/response';
import * as _ from 'underscore';

import { DevicesService } from '../../services/devices.service';
import { DeviceDetail } from '../../models/device-detail.model';
import { GetDevicesResponse } from '../../models/get-device-response.model';
import { PaginationProperties } from '../../models/pagination-properties.model';
import { ErrorMessage } from '../../models/error-message.model';
import { Router } from '@angular/router';

@Component({
   selector: 'my-devices',
   templateUrl: './my-devices.component.html'
})

export class MyDevicesComponent {
   title = "My Devices";
   workInProgress: boolean = false;
   getDevicesResponse: GetDevicesResponse = new GetDevicesResponse;

   /* The following are used for pagination navigation. */
   paginationProperties: PaginationProperties = new PaginationProperties;

   @Input() devices: DeviceDetail[] = [];

   constructor(private router: Router, private deviceService: DevicesService) {
   }

   ngOnInit() {
      this.getDevices(0, 0);
   }

   setPage(page: number) {
      if (this.paginationProperties.currentPage == page) return;

      if (page < 1) return;

      if (page > this.paginationProperties.totalPages) return;

      this.getDevices(this.paginationProperties.pageSize, this.getFirstItemIndexOfPage(page));
   }

   updatePaginationDetails(offset: number, limit: number, size: number) {
      this.paginationProperties.pageSize = limit;
      this.paginationProperties.totalItems = size;
      this.paginationProperties.totalPages = Math.ceil(size / limit);
      this.paginationProperties.currentPage = (offset / limit) + 1;

      if (this.paginationProperties.totalPages <= 10) {
         /* Less than 10 total pages so show all. */
         this.paginationProperties.startPage = 1;
         this.paginationProperties.endPage = this.paginationProperties.totalPages;
      }
      else {
         /* More than 10 total pages so calculate start and end pages. */
         if (this.paginationProperties.currentPage <= 6) {
            this.paginationProperties.startPage = 1;
            this.paginationProperties.endPage = 10;
         } else if (this.paginationProperties.currentPage + 4 >= this.paginationProperties.totalPages) {
            this.paginationProperties.startPage = this.paginationProperties.totalPages - 9;
            this.paginationProperties.endPage = this.paginationProperties.totalPages;
         } else {
            this.paginationProperties.startPage = this.paginationProperties.currentPage - 5;
            this.paginationProperties.endPage = this.paginationProperties.currentPage + 4;
         }
      }

      /* Calculate start and end item indexes. */
      this.paginationProperties.startIndex = (this.paginationProperties.currentPage - 1) * this.paginationProperties.pageSize;
      this.paginationProperties.endIndex = Math.min(this.paginationProperties.startIndex + this.paginationProperties.pageSize - 1, this.paginationProperties.totalItems - 1);

      /* Create an array of pages to ng-repeat in the pager control. */
      this.paginationProperties.pages = _.range(this.paginationProperties.startPage, this.paginationProperties.endPage + 1);
   }

   getFirstItemIndexOfPage(page: number) {
      /* Return the 0-indexed first item of current page. */
      return (page * this.paginationProperties.pageSize) - this.paginationProperties.pageSize;
   }

   private getDevices(limit: number, offset: number) {
      this.workInProgress = true;
      this.deviceService.getDevices(limit, offset)
         .subscribe(
         (data: GetDevicesResponse | ErrorMessage) => {
            if (data instanceof GetDevicesResponse) {
               this.getDevicesResponse = data
            }
         },
         (error: ErrorMessage) => {
            this.handleError(error)
         },
         () => {
            this.updatePaginationDetails(this.getDevicesResponse.offset, this.getDevicesResponse.limit, this.getDevicesResponse.size);
            this.devices = this.getDevicesResponse.value;   /* Value contains devices. */
            this.workInProgress = false;
         });
   }

   private handleError(error: ErrorMessage) {

      this.workInProgress = false;
      this.router.navigate(['error', { type: error.errorType, code: String(error.errorCode) }]);
   }
}
