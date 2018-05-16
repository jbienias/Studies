require 'q_2_4'

describe "#unzip" do
  it "deconstructs nested array" do
    nums = [1, 2, 3]
    lets = %w|a b c|
    zipped = nums.zip(lets)
    expected = [["a", "b", "c"], [1, 2, 3]]
    expect(zipped.unzip).to eq expected
  end
end
