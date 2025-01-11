import {Answer} from "./answer";

enum QuestionType {
    Choice,
    MultipleChoice,
    TrueFalse,
    Baisc
  }


export type Question = {
    Id: string,
    QuestionText: string | null,
    adminId: string,
    Level: number,
    Type: QuestionType ,
    PredefinedAnswers: Answer[]
};
