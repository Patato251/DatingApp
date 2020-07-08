import { Photo } from './Photo';

export interface User {
  // Properties for User Dto's from API
  id: number;
  usernmae: string;
  knownAs: string;
  age: number;
  gender: string;
  created: Date;
  lastActive: Date;
  photoUrl: string;
  city: string;
  country: string;

  // Optional Properties
  interests?: string;
  introduction?: string;
  lookingFor?: string;
  photos?: Photo[];
}
