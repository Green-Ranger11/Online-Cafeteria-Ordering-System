import { IMeal } from './meal';

export interface IPagination {
  pageIndex: number;
  pageSize: number;
  count: number;
  data: IMeal[];
}
