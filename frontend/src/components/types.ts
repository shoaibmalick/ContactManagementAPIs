export interface Employee {
  id: number;
  name: string;
  jobTitle: string;
  email: string;
  phone: string;
  isActive: boolean;
  companyName?: string;
  emailStatus?: "DELIVERABLE" | "UNDELIVERABLE" | "UNKNOWN";
}

export interface PagedResult<T> {
  items: T[];
  totalCount: number;
}
