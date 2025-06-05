export interface Client {
    id?: number,
    firstName: string;
    lastName: string;
    email: string;
    password?: string;
    confirmPassword?: string;
    address: string;
    phoneNumber: number;
}