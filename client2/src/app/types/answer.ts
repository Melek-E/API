enum AnswerType {
  Choice,
  MultipleChoice,
  TrueFalse
}


export type Answer= {
    Id: number,
    AnswerText: string,
    IsCorrect: boolean,
    Type: AnswerType ,
    QuestionId: number,
    UserId: string,
}
