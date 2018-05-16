require "q_1_2"

describe "#average_word_length" do
  it "returns average word length" do
    string = "i wish that i was cool"
    expect(average_word_length(string)).to be_within(0.01).of 2.83
  end

  it "returns nil for empty string" do
    expect(average_word_length("")).to be_nil
  end
end
