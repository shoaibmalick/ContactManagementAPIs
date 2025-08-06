import axios from "axios";

const ABSTRACT_API_KEY = import.meta.env.VITE_ABSTRACT_API_KEY; 

export interface EmailValidationResult {
  deliverability: "DELIVERABLE" | "UNDELIVERABLE" | "UNKNOWN";
  is_valid_format: boolean;
  is_disposable_email: boolean;
  is_free_email: boolean;
  quality_score: string;
}


export const validateEmail = async (email: string): Promise<EmailValidationResult | null> => {
  try {
    const response = await axios.get("https://emailvalidation.abstractapi.com/v1/", {
      params: {
        api_key: ABSTRACT_API_KEY,
        email,
      },
    });
    console.log("Validating email:", email);
    console.log("Using API Key:", ABSTRACT_API_KEY);

    return {
      deliverability: response.data.deliverability,
      is_valid_format: response.data.is_valid_format.value,
      is_disposable_email: response.data.is_disposable_email.value,
      is_free_email: response.data.is_free_email.value,
      quality_score: response.data.quality_score,
    };
  } 
  catch (error: any) {
    if (axios.isAxiosError(error) && error.response) {
        console.log("Validating email:", email);
        console.log("Using API Key:", ABSTRACT_API_KEY);
      console.error("Email validation failed - status:", error.response.status);
      console.error("Response data:", error.response.data);
    } else {
        console.log("Validating email:", email);
        console.log("Using API Key:", ABSTRACT_API_KEY);
      console.error("Unexpected error:", error);
    }
    return null;
  
  }
};
