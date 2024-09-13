import { Messages } from './messages';

import {Question} from "./questions";

import {Answer} from "./answer";



export const testStatusList: string[] = [
  'Open',
  'In Progress',
  'Completed',
];

export const testDiffList: string[] = [
  '1',
  '2',
  '3',
];


export type TestStatus = (typeof testStatusList)[number];

export type Test = {
  Id: number,
  Timestamp: string | Date | number,
  SubmittedAt: string | Date | number|null,
  Score: number,
  UserId: string,
  Questions: Question[],
  //company: string,
  status: TestStatus,

};


export const newTest: Test = {
  Id: 0,
  Timestamp: new Date(),
  SubmittedAt: null,
  Score: 0,
  UserId: '',
  status: 'Open',
  Questions: [],
};
