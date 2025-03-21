

export const contactStatusList: string[] = [
  'Salaried',
  'Commission',
  'Terminated',
];

export type ContactStatus = (typeof contactStatusList)[number];

type State = {
    stateShort: string;
};

export interface ContactBase {
  address: string,
  firstName: string,
  lastName: string,
  position: string,
  manager: string,
  company: string,
  phone: string,
  email: string,
  image: string,
}

export interface Contact extends ContactBase {
  id: number,
  name: string,
  status: ContactStatus,
  company: string,
  city: string,
  state: State,
  zipCode: number

}

export const newContact: ContactBase = {
  firstName: '',
  lastName: '',
  position: '',
  manager: '',
  company: '',
  phone: '',
  email: '',
  image: '',
  address: '',
}
