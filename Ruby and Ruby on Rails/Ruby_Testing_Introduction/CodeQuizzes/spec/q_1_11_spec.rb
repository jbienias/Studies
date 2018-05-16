require 'q_1_11'

describe "#acronym?" do
  it "returns true if word is an acronym" do
    words = "what you see is what you get"
    expect(words.acronym?("wysiwyg")).to be true
  end

  it "returns false if word is not an acronym" do
    words = "your paper has way too many acronyms"
    expect(words.acronym?("blah")).to be false
  end
end
