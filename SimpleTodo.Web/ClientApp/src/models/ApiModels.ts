import { SerializedError } from "@reduxjs/toolkit";

export interface ApiError extends SerializedError {
  data?: any;
  httpStatus?: number;
}
